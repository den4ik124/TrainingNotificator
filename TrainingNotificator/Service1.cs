using System;
using System.ServiceProcess;
using System.Threading.Tasks;
using TrainingNotificator.Bot;
using TrainingNotificator.Core.Interfaces;
using TrainingNotificator.Logger;

namespace TrainingNotificator
{
    public partial class Service1 : ServiceBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger logger;
        private TelegramBot bot;

        public Service1(IUnitOfWork unitOfWork, ILogger logger)
        {
            InitializeComponent();
            this.unitOfWork = unitOfWork;
            this.logger = logger;
        }

        //public Service1()
        //{
        //    InitializeComponent();
        //}
        public void onDebug()
        {
            OnStart(null);
        }

        protected override void OnStart(string[] args)
        {
            Task.Factory.StartNew(async () =>
            {
                this.bot = new TelegramBot(unitOfWork);

                while (true)
                {
                    var currentDateTime = DateTime.Now;
                    if (IsCheckWorkingTime(currentDateTime))
                    {
                        return;
                    }
#if DEBUG

                    try
                    {
                        var users = await this.unitOfWork.UsersRepository.GetAll();

                        foreach (var user in users)
                        {
                            await bot.SendMessage(user.Id, "ПОРА ПОЗАНИМАТЬСЯ !!!");
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.Log(ex);
                    }
#else

                    try
                    {
                        if (currentDateTime.Second == 0)
                        {
                            var users = await this.unitOfWork.UsersRepository.GetAll();

                            foreach (var user in users)
                            {
                                await bot.SendMessage(user.Id, "ПОРА ПОЗАНИМАТЬСЯ !!!");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.Log(ex);
                    }
#endif

                    Console.WriteLine($"Проверка времени. {DateTime.Now}");
                    await Task.Delay(1000);
                }
            });
        }

        private bool IsCheckWorkingTime(DateTime currentDateTime)
        {
            return currentDateTime.DayOfWeek == DayOfWeek.Saturday
                || currentDateTime.DayOfWeek == DayOfWeek.Sunday
                || currentDateTime.Hour < 10
                || currentDateTime.Hour > 19;
        }
    }
}