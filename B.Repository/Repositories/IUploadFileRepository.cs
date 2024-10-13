using A.Domain.Entities;

namespace B.Repository.Repositories;

public interface IUploadFileRepository
{
    Task AddAsync(UploadFile uploadFile, CancellationToken cancellationToken);
    Task<UploadFile> GetByIdAsync(int id, CancellationToken cancellationToken);
}
