using A.Domain.Entities;

namespace B.Repository.Repositories;

public interface IUploadFileRepository
{
    Task AddAsync(UploadFile uploadFile);
    Task<UploadFile> GetByIdAsync(int id);
}
