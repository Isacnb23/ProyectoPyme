public class TableColumn
{
    public string Key { get; set; } = "";
    public string Label { get; set; } = "";
    public string? Class { get; set; }
}

public class TableViewModel
{
    public string ControllerName { get; set; } = ""; // for action links
    public List<TableColumn> Columns { get; set; } = new();
    public IEnumerable<object>? Rows { get; set; } // generic: any model
}
