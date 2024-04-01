namespace PhotoCloud.DatabaseClient.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class TableAttribute : Attribute
    {
        public string Table { get; set; }

        public TableAttribute(string table)
        {
            this.Table = table;
        }
    }
}
