using ChatApp.Persistence.DbContexts;
using ChatApp.Persistence.Repositories.Interfaces;

namespace ChatApp.Persistence.Repositories.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ChatContext context;
        public UnitOfWork(ChatContext context)
        {
            this.context = context;
        }

        public async Task<int> SaveChangesAsync()
        {

            return await context.SaveChangesAsync();
        }
    }
}
