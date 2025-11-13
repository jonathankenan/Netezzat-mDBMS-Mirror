namespace mDBMS.Common.Models;

public class DataRetrieval
{
    public string Table { get; set; }
    public required string[] Columns { get; set; }
    public string? Condition { get; set; }

    public DataRetrieval(string table, string[] columns, string? condition = null)
    {
        Table = table;
        Columns = columns;
        Condition = condition;
    }
}