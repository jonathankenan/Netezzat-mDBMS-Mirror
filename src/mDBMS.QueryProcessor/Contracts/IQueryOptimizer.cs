namespace mDBMS.QueryProcessor.Contracts
{
    public interface IQueryOptimizer
    {
        ParsedQuery OptimizeQuery(string query);

        int GetCost(ParsedQuery query);
    }
}
