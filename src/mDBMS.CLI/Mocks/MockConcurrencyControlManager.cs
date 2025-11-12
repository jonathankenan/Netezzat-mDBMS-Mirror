using System;
using System.Threading;
using mDBMS.QueryProcessor.Contracts;

namespace mDBMS.CLI.Mocks
{
    public class MockConcurrencyControlManager : IConcurrencyControlManager
    {
        private int _transactionCounter = 1000;

        public int BeginTransaction()
        {
            var id = Interlocked.Increment(ref _transactionCounter);
            Console.WriteLine($"[MOCK CCM]: BeginTransaction dipanggil. ID = {id}");
            return id;
        }

        public ConcurrencyResponse ValidateObject(TransactionAction action)
        {
            Console.WriteLine($"[MOCK CCM]: ValidateObject dipanggil untuk operasi '{action.Operation}' pada '{action.Target}'.");
            return new ConcurrencyResponse
            {
                Allowed = true,
                Message = "Selalu diizinkan pada stub."
            };
        }

        public void EndTransaction(int transactionId, TransactionStatus status)
        {
            Console.WriteLine($"[MOCK CCM]: EndTransaction dipanggil. ID = {transactionId}, status = {status}");
        }
    }
}
