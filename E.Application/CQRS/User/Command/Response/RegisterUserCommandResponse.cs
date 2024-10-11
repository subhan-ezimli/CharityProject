namespace E.Application.CQRS.User.Command.Response;

public record struct RegisterUserCommandResponse
{
    public string Message { get; set; }
}
