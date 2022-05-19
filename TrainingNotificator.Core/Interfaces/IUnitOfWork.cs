using System.Threading.Tasks;
using TrainingNotificator.Core.Models;

namespace TrainingNotificator.Core.Interfaces
{
    public interface IUnitOfWork : ITransaction
    {
        IRepository<CustomUser> UsersRepository { get; }

        Task CompleteAsync();
    }
}