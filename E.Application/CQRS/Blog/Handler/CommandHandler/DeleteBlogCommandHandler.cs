using B.Repository.Common;
using C.Common.Exceptions;
using C.Common.GlobalResponses.Generics;
using E.Application.CQRS.Blog.Command.Request;
using E.Application.CQRS.Blog.Command.Response;
using MediatR;

namespace E.Application.CQRS.Blog.Handler.CommandHandler;

public class DeleteBlogCommandHandler : IRequestHandler<DeleteBlogCommandRequest, TypedResponseModel<DeleteBlogCommandResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    public DeleteBlogCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<TypedResponseModel<DeleteBlogCommandResponse>> Handle(DeleteBlogCommandRequest request, CancellationToken cancellationToken)
    {
        var blog = await _unitOfWork.BlogRepository.GetByIdAsync(request.Id, cancellationToken);
        if (blog == null)
        {
            throw new BadRequestException("not Found");
        }

        await _unitOfWork.BlogRepository.DeleteAsync(blog, cancellationToken);
        await _unitOfWork.SaveChanges(cancellationToken);
        return new TypedResponseModel<DeleteBlogCommandResponse>
        {
            Data = new DeleteBlogCommandResponse
            {
                Message = "Succesfully deleted"
            }
        };
    }
}
