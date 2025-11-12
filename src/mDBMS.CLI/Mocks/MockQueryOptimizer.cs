using System;
using System.Collections.Generic;
using mDBMS.QueryProcessor.Contracts;

namespace mDBMS.CLI.Mocks
{
    public class MockQueryOptimizer : IQueryOptimizer
    {
        public ParsedQuery OptimizeQuery(string query)
        {
            Console.WriteLine($"[MOCK QO]: OptimizeQuery dipanggil dengan '{query}'.");
            return new ParsedQuery
            {
                RawQuery = query,
                QueryType = "DML",
                Metadata = new Dictionary<string, string>
                {
                    { "info", "dummy plan" }
                }
            };
        }

        public int GetCost(ParsedQuery query)
        {
            Console.WriteLine("[MOCK QO]: GetCost dipanggil.");
            return 1;
        }
    }
}
