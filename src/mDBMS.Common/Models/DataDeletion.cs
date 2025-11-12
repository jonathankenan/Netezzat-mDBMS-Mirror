namespace mDBMS.Common.Models;

public class DataDeletion
{
    public string Table { get; set; }
    public string? Condition { get; set; }

    public DataDeletion(string table, string? condition = null)
    {
        Table = table;
        Condition = condition;
    }
}