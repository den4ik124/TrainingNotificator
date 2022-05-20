using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using TrainingNotificator.Core.Interfaces;
using TrainingNotificator.Data;
using TrainingNotificator.Logger;

namespace TrainingNotificator
{
    internal static class Program
    {
        private static ILogger logger = new Logger.Logger();

        private static async Task Main(string[] args)
        {
            try
            {
                var host = Host.CreateDefaultBuilder(args).ConfigureServices((_, services) =>
                        services.AddSingleton<UsersDbContext>()
                        .AddTransient<IUnitOfWork, UnitOfWork>()
                        .AddSingleton<ILogger, Logger.Logger>()
                    ).Build();

                var unitOfWorkService = host.Services.GetRequiredService<IUnitOfWork>();
#if DEBUG
                var service = new Service1(unitOfWorkService, logger);
                service.onDebug();
#else
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[]
                {
                    new Service1(unitOfWorkService,logger)
                };
                ServiceBase.Run(ServicesToRun);
#endif

                await host.RunAsync();
            }
            catch (System.Exception ex)
            {
                logger.Log(ex);
            }
        }
    }
}