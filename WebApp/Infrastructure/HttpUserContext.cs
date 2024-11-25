using C.Common.Exceptions;
using E.Application.Security;
using System.Security.Claims;

namespace WebApp.Infrastructure;

public class HttpUserContext : IUserContext
{
    private readonly int? _userId;

    public HttpUserContext(IHttpContextAccessor httpContextAccessor)
    {
        var id = httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

        bool isOkay = int.TryParse(id, out var x);
        _userId = isOkay ? x : null;

    }
    public int? UserId => _userId;

    public int MustGetUserId()
    {
        if (_userId is null)
            throw new InvalidClientException("User has to login!");

        return _userId!.Value;
    }
}