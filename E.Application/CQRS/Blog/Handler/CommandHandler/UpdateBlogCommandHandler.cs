using B.Repository.Common;
using C.Common.Exceptions;
using C.Common.GlobalResponses.Generics;
using E.Application.CQRS.Blog.Command.Request;
using E.Application.CQRS.Blog.Command.Response;
using E.Application.CQRS.Project.Command.Response;
using MediatR;

namespace E.Application.CQRS.Blog.Handler.CommandHandler;

public class UpdateBlogCommandHandler : IRequestHandler<UpdateBlogCommandRequest, TypedResponseModel<UpdateBlogCommandResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    public UpdateBlogCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }


    public async Task<TypedResponseModel<UpdateBlogCommandResponse>> Handle(UpdateBlogCommandRequest request, CancellationToken cancellationToken)
    {

        var blog = await _unitOfWork.BlogRepository.GetByIdAsync(request.Id, cancellationToken);
        if (blog == null && blog.IsDeleted)
        {
            throw new BadRequestException("not found");
        }

        blog.Header = request.Header;
        blog.UploadFileId = request.UploadFileId;
        blog.Content = request.Content;

        await _unitOfWork.BlogRepository.UpdateAsync(blog);
        await _unitOfWork.SaveChanges(cancellationToken);

        return new TypedResponseModel<UpdateBlogCommandResponse>
        {
            Data = new UpdateBlogCommandResponse
            {
                Message = "successfully updated"
            }
        };
    }
}
