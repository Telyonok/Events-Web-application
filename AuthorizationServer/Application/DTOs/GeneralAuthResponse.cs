namespace AuthorizationServer.Application.DTOs
{
    public class GeneralAuthResponse
    {
        public GeneralAuthResponse(bool success, string message, string? accessToken, string? refreshToken)
        {
            Success = success;
            Message = message ?? throw new ArgumentNullException(nameof(message));
            Token = new Token(accessToken, refreshToken);
        }

        public GeneralAuthResponse(string message) :
            this(false, message, null, null)
        { }

        public bool Success { get; set; }
        public string Message { get; set; }
        public Token Token { get; set; }
    }
}
