using PhotoCloud.DatabaseClient.Attributes;

namespace PhotoCloud.Model.Database
{
    [Table("User")]
    public class User
    {
        [PrimaryKey("ID")]
        public required int ID { get; set; }

        [Column("Name")]
        public required string Name { get; set; }

        [Column("Email")]
        public required string Email { get; set; }

        [Column("Password")]
        public required string Password { get; set; }

        [Column("CreationTime")]
        public required DateTime CreationTime { get; set; }
    }
}
