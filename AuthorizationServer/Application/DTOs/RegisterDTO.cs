namespace AuthorizationServer.Application.DTOs
{
    public class RegisterDTO
    {
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string UserName { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public List<string> Roles { get; set; } = new List<string>();
    }
}
