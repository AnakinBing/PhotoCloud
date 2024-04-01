namespace PhotoCloud.Model.Core
{
    public class AuthInfo
    {
        public int ID { get; set; }

        public string? UserName { get; set; }

        public string? Email { get; set; }

        public int Role { get; set; }

        public long Expiration { get; set; }
    }
}
