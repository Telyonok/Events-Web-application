namespace AuthorizationServer.Domain.Models
{
    public class AppSettings
    {
        public string EncryptionKey { get; set; } = string.Empty;
        public string ValidIssuer { get; set; } = string.Empty;
        public string ValidAudience { get; set; } = string.Empty;
    }
}
