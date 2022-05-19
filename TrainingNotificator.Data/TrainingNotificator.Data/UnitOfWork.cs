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

        public Task BeginTransactionAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task CompleteAsync()
        {
            throw new System.NotImplementedException();
        }

        public void RollbackTransaction()
        {
            throw new System.NotImplementedException();
        }

        public void TransactionCommit()
        {
            throw new System.NotImplementedException();
        }
    }
}