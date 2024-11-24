namespace E.Application.CQRS.User.Command.Response;

public class LoginUserCommandResponse
{
    public string Token { get; set; }
    public DateTime Expiration { get; set; }
    public int UserRole { get; set; }
}
