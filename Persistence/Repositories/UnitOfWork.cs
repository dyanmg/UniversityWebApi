using Application.Repositories;

namespace Persistence.Repositories
{
    internal class UnitOfWork(SchoolContext _schoolContext) : IUnitOfWork
    {
        public async Task SaveChanges()
        {
            await _schoolContext.SaveChangesAsync();
        }
    }
}
