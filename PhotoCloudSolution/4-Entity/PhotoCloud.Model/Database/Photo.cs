using PhotoCloud.DatabaseClient.Attributes;

namespace PhotoCloud.Model.Database
{
    [Table("Photo")]
    public class Photo
    {
        [PrimaryKey("ID")]
        public required int ID { get; set; }

        [Column("MD5")]
        public required string MD5 { get; set; }

        [Column("Name")]
        public required string Name { get; set; }

        [Column("Width")]
        public required int Width { get; set; }

        [Column("Height")]
        public required int Height { get; set; }

        [Column("Path")]
        public required string Path { get; set; }

        [Column("User")]
        public int User { get; set; }

        [Column("CreationTime")]
        public required DateTime CreationTime { get; set; }
    }
}
