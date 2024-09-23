namespace E.Application.Security;

public interface IUSerContext
{
    public int? UserId { get; }

    public int MustGetUserId();
}
