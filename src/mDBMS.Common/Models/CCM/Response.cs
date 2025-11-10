namespace mDBMS.Common.Models
{
    /// <summary>
    /// DTO yang merepresentasikan respons dari Concurrency Control Manager.
    /// Sesuai spesifikasi halaman 5.
    /// </summary>
    public class Response
    {
        /// <summary>
        /// Menentukan apakah aksi diizinkan (true) atau tidak (false).
        [cite_start]/// [cite: 758]
        /// </summary>
        public bool allowed { get; set; }

        /// <summary>
        /// ID Transaksi yang terkait dengan respons ini.
        [cite_start]/// [cite: 759]
        /// </summary>
        public int transaction_id { get; set; }
    }
}