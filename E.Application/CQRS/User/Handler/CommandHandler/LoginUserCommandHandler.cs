using B.Repository.Common;
using C.Common.Exceptions;
using C.Common.Extensions;
using C.Common.GlobalResponses.Generics;
using E.Application.CQRS.User.Command.Request;
using E.Application.CQRS.User.Command.Response;
using MediatR;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace E.Application.CQRS.User.Handler.CommandHandler;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, TypedResponseModel<LoginUserCommandResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IConfiguration _configuration;

    public LoginUserCommandHandler(IUnitOfWork unitOfWork, IConfiguration configuration)
    {
        _unitOfWork = unitOfWork;
        _configuration = configuration;
    }

    public async Task<TypedResponseModel<LoginUserCommandResponse>> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
    {
        var loginProvider = Guid.NewGuid().ToString(); // Different browsers can have the same user, use this to differentiate them
        var user = await _unitOfWork.UserRepository.FindByEmailAsync(request.Email, cancellationToken);

        if (user == null)
            throw new NotFoundException(typeof(A.Domain.Entities.User), request.Email);

        var hashedPassword = PasswordHasher.ComputeStringToSha256Hash(request.Password);
        if (user != null && (hashedPassword == user.PasswordHash))
        {
            var authClaims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(ClaimTypes.Name, user.Name),
                new("loginProvider", loginProvider),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.UserRole.ToString())
            };

            var token = TokenService.CreateToken(authClaims, _configuration);
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token); // Convert the token to a string

            var loginUserCommandResponse = new LoginUserCommandResponse() { Token = tokenString, Expiration = token.ValidTo, UserRole = (int)user.UserRole };

            return new TypedResponseModel<LoginUserCommandResponse>
            {
                Data = loginUserCommandResponse
            };
        }
        throw new BadRequestException("Entered Password is wrong");
    }
}
