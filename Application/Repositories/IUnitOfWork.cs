namespace Application.Repositories
{
    public interface IUnitOfWork
    {
        public Task SaveChanges();
    }
}
