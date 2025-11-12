namespace mDBMS.QueryProcessor.Contracts
{
    /// <summary>
    /// DTO respon sederhana dari CCM.
    /// </summary>
    public class ConcurrencyResponse
    {
        public bool Allowed { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
