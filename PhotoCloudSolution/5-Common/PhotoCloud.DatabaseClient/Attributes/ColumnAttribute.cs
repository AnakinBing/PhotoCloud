namespace PhotoCloud.DatabaseClient.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ColumnAttribute : Attribute
    {
        public string Column { get; set; }

        public ColumnAttribute(string column)
        {
            this.Column = column;
        }
    }
}
