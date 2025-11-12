using System.Collections.Generic;

namespace mDBMS.QueryProcessor.Contracts
{
    /// <summary>
    /// Representasi sederhana hasil parsing optimizer untuk fase awal.
    /// </summary>
    public class ParsedQuery
    {
        public string RawQuery { get; set; } = string.Empty;
        public string QueryType { get; set; } = string.Empty;
        public Dictionary<string, string> Metadata { get; set; } = new();
    }
}
