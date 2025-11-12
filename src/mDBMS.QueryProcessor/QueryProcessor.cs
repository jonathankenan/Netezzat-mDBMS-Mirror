using System;
using System.Linq;
using mDBMS.Common.Interfaces;
using mDBMS.Common.Models;
using mDBMS.QueryProcessor.Contracts;

namespace mDBMS.QueryProcessor
{
    /// <summary>
    /// kelas utama Query Processor untuk fase 1: parsing sederhana lalu routing ke komponen lain.
    /// </summary>
    public class QueryProcessor
    {
        private readonly IQueryOptimizer _optimizer;
        private readonly IConcurrencyControl _concurrencyControl;
        private readonly IFailureRecovery _failureRecovery;

        private int? _activeTransactionId;

        public QueryProcessor(
            IQueryOptimizer optimizer,
            IConcurrencyControl concurrencyControl,
            IFailureRecovery failureRecovery)
        {
            _optimizer = optimizer ?? throw new ArgumentNullException(nameof(optimizer));
            _concurrencyControl = concurrencyControl ?? throw new ArgumentNullException(nameof(concurrencyControl));
            _failureRecovery = failureRecovery ?? throw new ArgumentNullException(nameof(failureRecovery));
        }

        public ExecutionResult ExecuteQuery(string? query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return LogAndReturn(BuildResult(string.Empty, success: false, message: "Query tidak boleh kosong."));
            }

            var normalizedQuery = query.Trim();
            try
            {
                var classification = Classify(normalizedQuery);
                var result = classification switch
                {
                    QueryClassification.Dml => HandleDmlQuery(normalizedQuery),
                    QueryClassification.TransactionBegin => HandleBeginTransaction(normalizedQuery),
                    QueryClassification.TransactionCommit => HandleCommitTransaction(normalizedQuery),
                    QueryClassification.TransactionAbort => HandleAbortTransaction(normalizedQuery),
                    _ => BuildResult(normalizedQuery, false, "Perintah tidak dikenali. Tulis SELECT/INSERT/UPDATE/DELETE atau perintah transaksi."),
                };

                return LogAndReturn(result);
            }
            catch (Exception ex)
            {
                return LogAndReturn(BuildResult(normalizedQuery, false, $"Kesalahan internal Query Processor: {ex.Message}"));
            }
        }

        private ExecutionResult HandleDmlQuery(string query)
        {
            var parsed = _optimizer.ParseQuery(query);
            _optimizer.OptimizeQuery(parsed, Enumerable.Empty<Statistic>());
            return BuildResult(query, true, "Query berhasil diparse dan diteruskan ke Query Optimizer.");
        }

        private ExecutionResult HandleBeginTransaction(string query)
        {
            if (_activeTransactionId.HasValue)
            {
                return BuildResult(query, false, $"Masih ada transaksi aktif dengan ID {_activeTransactionId.Value}.");
            }

            _activeTransactionId = _concurrencyControl.begin_transaction();
            return BuildResult(query, true, $"Transaksi baru dimulai dengan ID {_activeTransactionId.Value}.");
        }

        private ExecutionResult HandleCommitTransaction(string query)
        {
            if (!_activeTransactionId.HasValue)
            {
                return BuildResult(query, false, "Tidak ada transaksi aktif yang bisa di-COMMIT.");
            }

            var transactionId = _activeTransactionId.Value;
            _concurrencyControl.end_transaction(transactionId);
            _activeTransactionId = null;
            return BuildResult(query, true, $"Transaksi {transactionId} berhasil di-COMMIT.");
        }

        private ExecutionResult HandleAbortTransaction(string query)
        {
            if (!_activeTransactionId.HasValue)
            {
                return BuildResult(query, false, "Tidak ada transaksi aktif yang bisa di-ABORT.");
            }

            var transactionId = _activeTransactionId.Value;
            _concurrencyControl.end_transaction(transactionId);
            _activeTransactionId = null;
            return BuildResult(query, true, $"Transaksi {transactionId} berhasil di-ABORT.");
        }

        private ExecutionResult LogAndReturn(ExecutionResult result)
        {
            _failureRecovery.WriteLog(result);
            return result;
        }

        private static ExecutionResult BuildResult(string query, bool success, string message)
        {
            return new ExecutionResult
            {
                Query = query,
                Success = success,
                Message = message
            };
        }

        private static QueryClassification Classify(string query)
        {
            var upper = query.TrimStart().ToUpperInvariant();

            if (upper.StartsWith("BEGIN"))
            {
                return QueryClassification.TransactionBegin;
            }

            if (upper.StartsWith("COMMIT"))
            {
                return QueryClassification.TransactionCommit;
            }

            if (upper.StartsWith("ROLLBACK") || upper.StartsWith("ABORT"))
            {
                return QueryClassification.TransactionAbort;
            }

            if (upper.StartsWith("SELECT") ||
                upper.StartsWith("INSERT") ||
                upper.StartsWith("UPDATE") ||
                upper.StartsWith("DELETE"))
            {
                return QueryClassification.Dml;
            }

            return QueryClassification.Unknown;
        }

        private enum QueryClassification
        {
            Unknown = 0,
            Dml,
            TransactionBegin,
            TransactionCommit,
            TransactionAbort
        }
    }
}
