namespace PhotoCloud.DatabaseClient.Model
{
    public class TableModel
    {
        public string? Name { get; set; }

        public string? PrimaryKey { get; set; }

        public IList<string>? Columns { get; set; }
    }
}
