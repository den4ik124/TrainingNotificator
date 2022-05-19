using System;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types.Enums;
using TrainingNotificator.Bot.Constants;

namespace TrainingNotificator.Bot
{
    public class TelegramBot
    {
        private TelegramBotClient botClient;

        public TelegramBot()
        {
            botClient = new TelegramBotClient(TelegramBotConstants.Token);
            var cts = new CancellationTokenSource();
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = Array.Empty<UpdateType>() // receive all update types
            };
            botClient.StartReceiving(
                updateHandler: MessageHandler.HandleUpdateAsync,
                errorHandler: MessageHandler.HandlePollingErrorAsync,
                receiverOptions: receiverOptions,
                cancellationToken: cts.Token
            );
        }

        public void Start()
        {
            while (true)
            {
                var currentDateTime = DateTime.Now;
                if (IsCheckWorkingTime(currentDateTime))
                {
                    return;
                }

            }
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