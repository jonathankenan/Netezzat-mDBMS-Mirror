namespace mDBMS.Common.Models
{
    /// <summary>
    /// DTO yang merepresentasikan sebuah aksi (read/write)
    /// Sesuai spesifikasi halaman 5.
    /// </summary>
    public class Action
    {
        /// <summary>
        /// Tipe aksi yang akan dilakukan, 'write' atau 'read'.
        [cite_start]/// [cite: 757]
        /// </summary>
        public string action { get; set; } = string.Empty;
    }
}