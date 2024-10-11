using B.Repository.Repositories;

namespace B.Repository.Common
{
    public interface IUnitOfWork
    {
        public IUserRepository UserRepository { get; }
        public IUploadFileRepository UploadFileRepository { get; }

        Task<int> SaveChanges();
    }
}
