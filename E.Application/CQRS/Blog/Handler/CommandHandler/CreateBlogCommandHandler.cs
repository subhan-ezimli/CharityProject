using B.Repository.Common;
using C.Common.GlobalResponses.Generics;
using E.Application.CQRS.Blog.Command.Request;
using E.Application.CQRS.Blog.Command.Response;
using E.Application.CQRS.Project.Command.Response;
using MediatR;
using Microsoft.EntityFrameworkCore.Metadata;

namespace E.Application.CQRS.Blog.Handler.CommandHandler;

public class CreateBlogCommandHandler : IRequestHandler<CreateBlogCommandRequest, TypedResponseModel<CreateBlogCommandResponse>>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateBlogCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<TypedResponseModel<CreateBlogCommandResponse>> Handle(CreateBlogCommandRequest request, CancellationToken cancellationToken)
    {
        var blog = new A.Domain.Entities.Blog();
        blog.Header = request.Header;
        blog.Content = request.Content;
        blog.UploadFileId = request.UploadFileId;

        await _unitOfWork.BlogRepository.AddAsync(blog, cancellationToken);
        await _unitOfWork.SaveChanges(cancellationToken);

        return new TypedResponseModel<CreateBlogCommandResponse>
        {
            Data = new CreateBlogCommandResponse
            {
                Message = "Successfully added"
            }
        };
    }
}
