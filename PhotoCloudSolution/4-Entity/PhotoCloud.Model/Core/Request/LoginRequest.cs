namespace PhotoCloud.Model.Core.Request
{
    public class LoginRequest
    {
        public required string Email { get; set; }

        public required string Password { get; set; }
    }
}
