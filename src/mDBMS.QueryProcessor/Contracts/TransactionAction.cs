namespace mDBMS.QueryProcessor.Contracts
{
    /// <summary>
    /// DTO sederhana untuk permintaan validasi CCM.
    /// </summary>
    public class TransactionAction
    {
        public int TransactionId { get; set; }
        public string Operation { get; set; } = string.Empty;
        public string Target { get; set; } = string.Empty;
    }
}
