namespace PhotoCloud.Model.Core.Response
{
    public class LoginResponse
    {
        public required int ID { get; set; }

        public required string Name { get; set; }

        public required string Email { get; set; }

        public required DateTime CreationTime { get; set; }

        public string? Token { get; set; }
    }
}
