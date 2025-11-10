using mDBMS.Common.Models;

namespace mDBMS.Common.Interfaces
{
    /// <summary>
    /// Kontrak untuk Concurrency Control Manager (CCM).
    /// Mendefinisikan method-method sesuai spesifikasi halaman 4.
    [cite_start]/// [cite: 732]
    /// </summary>
    public interface IConcurrencyControl
    {
        /// <summary>
        /// Memberikan transaction id pada transaksi yang baru dimulai.
        [cite_start]/// [cite: 738]
        /// </summary>
        /// <returns>Transaction ID (int)</returns>
        int begin_transaction();

        /// <summary>
        /// Mencatat (log) sebuah objek pada transaksi tertentu.
        [cite_start]/// [cite: 740]
        /// </summary>
        [cite_start]/// <param name="object">Objek 'Row' yang akan dicatat [cite: 740]</param>
        [cite_start]/// <param name="transaction_id">ID transaksi [cite: 740]</param>
        void log_object(Row @object, int transaction_id);

        /// <summary>
        /// Memvalidasi apakah suatu object diizinkan melakukan aksi tertentu.
        [cite_start]/// 
        /// </summary>
        [cite_start]/// <param name="object">Objek 'Row' yang akan divalidasi </param>
        [cite_start]/// <param name="transaction_id">ID transaksi </param>
        [cite_start]/// <param name="action">DTO 'Action' yang berisi tipe aksi </param>
        [cite_start]/// <returns>DTO 'Response' yang berisi status izin </returns>
        Response validate_object(Row @object, int transaction_id, Action action);

        /// <summary>
        /// Mengakhiri transaksi (commit atau abort).
        [cite_start]/// [cite: 751]
        /// </summary>
        [cite_start]/// <param name="transaction_id">ID transaksi yang akan diakhiri [cite: 751]</param>
        void end_transaction(int transaction_id);
    }
}