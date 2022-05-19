using System;
using System.IO;
using System.ServiceProcess;
using System.Threading.Tasks;

namespace TrainingNotificator
{
    public partial class Service1 : ServiceBase
    {
        //private readonly IUnitOfWork unitOfWork;

        //public Service1(IUnitOfWork unitOfWork)
        //{
        //    InitializeComponent();
        //    this.unitOfWork = unitOfWork;
        //}
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            //var bot = new TelegramBot(this.unitOfWork);
            //bot.Start().GetAwaiter();

            Task.Factory.StartNew(async () =>
            {
                while (true)
                {
                    var currentTime = DateTime.Now;

                    if (currentTime.Second % 5 == 0)
                    {
                        File.AppendAllText(@"D:\test\result.txt", $"Текущее время: {currentTime}\n");
                    }
                    await Task.Delay(1000);
                }
            });
        }
    }
}