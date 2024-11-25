using C.Common.GlobalResponses.Generics;
using E.Application.CQRS.User.Command.Response;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace E.Application.CQRS.User.Command.Request;

public class ChangePasswordCommandRequest : IRequest<TypedResponseModel<ChangePasswordCommandResponse>>
{
    public string CurrentPassword { get; set; }

    [Required]
    //[StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
   // [DataType(DataType.Password)]
    //[Display(Name = "NewPassword")]
    public string NewPassword { get; set; }


    //[DataType(DataType.Password)]
    //[Display(Name = "Confirm password")]
    //[Compare("NewPassword")]
    public string ConfirmPassword { get; set; }

}
