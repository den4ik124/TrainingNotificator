using System.Threading.Tasks;

namespace TrainingNotificator.Core.Interfaces
{
    public interface ITransaction
    {
        Task BeginTransactionAsync();

        void TransactionCommit();

        void RollbackTransaction();
    }
}