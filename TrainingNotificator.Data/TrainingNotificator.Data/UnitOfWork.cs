using System.Threading.Tasks;
using TrainingNotificator.Core.Interfaces;
using TrainingNotificator.Core.Models;

namespace TrainingNotificator.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly UsersDbContext context;

        public UnitOfWork(UsersDbContext context)
        {
            this.context = context;
            UsersRepository = new Repository<CustomUser>(context);
        }

        public IRepository<CustomUser> UsersRepository { get; set; }

        public async Task BeginTransactionAsync()
        {
            await this.context.Database.BeginTransactionAsync();
        }

        public void TransactionCommit()
        {
            this.context.Database.CommitTransaction();
        }

        public void RollbackTransaction()
        {
            this.context.Database.RollbackTransaction();
        }

        public async Task CompleteAsync()
        {
            await this.context.SaveChangesAsync();
        }

        public void Dispose()
        {
            this.context.Dispose();
        }
    }
}