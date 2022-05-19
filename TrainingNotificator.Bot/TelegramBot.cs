using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types.Enums;
using TrainingNotificator.Bot.Constants;
using TrainingNotificator.Core.Interfaces;

namespace TrainingNotificator.Bot
{
    public class TelegramBot
    {
        private readonly IUnitOfWork unitOfWork;
        private TelegramBotClient botClient;

        public TelegramBot(IUnitOfWork unitOfWork)
        {
            botClient = new TelegramBotClient(TelegramBotConstants.Token);
            var cts = new CancellationTokenSource();
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = Array.Empty<UpdateType>() // receive all update types
            };
            var messageHandler = new MessageHandler(unitOfWork);
            botClient.StartReceiving(
                updateHandler: messageHandler.HandleUpdateAsync,
                errorHandler: messageHandler.HandlePollingErrorAsync,
                receiverOptions: receiverOptions,
                cancellationToken: cts.Token
            );
            this.unitOfWork = unitOfWork;
        }

        public async Task Start()
        {
            while (true)
            {
                var currentDateTime = DateTime.Now;
                if (IsCheckWorkingTime(currentDateTime))
                {
                    return;
                }

                if (currentDateTime.Minute == 0)
                {
                    var users = await this.unitOfWork.UsersRepository.GetAll();
                    foreach (var user in users)
                    {
                        await botClient.SendTextMessageAsync(
                                chatId: user.Id,
                                text: "ПОРА ПОЗАНИМАТЬСЯ !!!",
                                cancellationToken: default);
                    }
                }

                Console.WriteLine($"Проверка времени. {DateTime.Now}");
                await Task.Delay(5000);
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