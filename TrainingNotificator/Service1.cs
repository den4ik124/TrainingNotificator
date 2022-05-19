using System.ServiceProcess;
using TrainingNotificator.Bot;
using TrainingNotificator.Core.Interfaces;

namespace TrainingNotificator
{
    public partial class Service1 : ServiceBase
    {
        private readonly IUnitOfWork unitOfWork;

        public Service1(IUnitOfWork unitOfWork)
        {
            InitializeComponent();
            this.unitOfWork = unitOfWork;
        }

        protected override void OnStart(string[] args)
        {
            var bot = new TelegramBot(this.unitOfWork);
            bot.Start().GetAwaiter();
        }

        protected override void OnStop()
        {
        }
    }
}