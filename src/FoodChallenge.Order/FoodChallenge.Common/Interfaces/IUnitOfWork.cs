namespace FoodChallenge.Common.Interfaces
{
    public interface IUnitOfWork
    {
        void BeginTransaction();
        Task CommitAsync();
        Task RollbackAsync();
    }
}

