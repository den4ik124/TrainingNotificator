using System.ServiceProcess;
using TrainingNotificator.Bot;

namespace TrainingNotificator
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            var bot = new TelegramBot();
            bot.Start();
        }

        protected override void OnStop()
        {
        }
    }
}