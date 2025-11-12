namespace mDBMS.QueryProcessor.Contracts
{
    public class ExecutionResult
    {
        public required string Query { get; set; }
        public bool Success { get; set; }
        public required string Message { get; set; }
    }
}
