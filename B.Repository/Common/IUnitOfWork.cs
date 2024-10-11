using B.Repository.Repositories;

namespace B.Repository.Common
{
    public interface IUnitOfWork
    {
        public IUserRepository UserRepository { get; }

        Task<int> SaveChanges();
    }
}
