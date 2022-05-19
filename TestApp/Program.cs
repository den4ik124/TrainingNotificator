using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using TrainingNotificator.Bot;
using TrainingNotificator.Core.Interfaces;
using TrainingNotificator.Data;

namespace TestApp
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args).ConfigureServices((_, services) =>
                services.AddDbContext<UsersDbContext>()
                        .AddTransient<IUnitOfWork, UnitOfWork>()
            ).Build();

            var unitOfWorkService = host.Services.GetRequiredService<IUnitOfWork>();
            var bot = new TelegramBot(unitOfWorkService);
            await bot.Start();
            await host.RunAsync();
        }

        //private static void Main(string[] args)
        //{
        //    var bot = new TelegramBot();
        //    bot.Start();
        //    Console.ReadLine();
        //}
    }
}