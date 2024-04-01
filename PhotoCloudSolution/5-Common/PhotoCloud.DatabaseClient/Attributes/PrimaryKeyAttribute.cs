namespace PhotoCloud.DatabaseClient.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class PrimaryKeyAttribute : Attribute
    {
        public string PrimaryKey { get; set; }

        public PrimaryKeyAttribute(string primaryKey)
        {
            this.PrimaryKey = primaryKey;
        }
    }
}
