using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TrainingNotificator.Core.Interfaces;
using TrainingNotificator.Core.Models;

namespace TrainingNotificator.Bot
{
    public class MessageHandler
    {
        private readonly IUnitOfWork unitOfWork;

        public MessageHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            // Only process Message updates: https://core.telegram.org/bots/api#message
            if (update.Type != UpdateType.Message)
                return;
            // Only process text messages
            if (update.Message.Type != MessageType.Text)
                return;

            var chatId = update.Message.Chat.Id;
            var messageText = update.Message.Text;

            if (messageText == "/start")
            {
                var user = await this.unitOfWork.UsersRepository.GetWhere(u => u.Id == chatId);
                if (user == null)
                {
                    var tgUser = update.Message.From;
                    var user = new CustomUser()
                    {
                        Id = tgUser.Id,
                        FirstName = tgUser.FirstName,
                        Username = tgUser.Username,
                    };
                    await this.unitOfWork.UsersRepository.Add(update.ChatMember.From)
                }
            }

            // Echo received message text
            Message sentMessage = await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: "You said:\n" + messageText,
                cancellationToken: cancellationToken);
        }

        public Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            if (exception is ApiRequestException)
            {
                var errorMessage = $"Telegram API Error:\n[{((ApiRequestException)exception).ErrorCode}]\n{((ApiRequestException)exception).Message}";
                Console.WriteLine(errorMessage);
                return Task.CompletedTask;
            }
            Console.WriteLine(exception.ToString());
            return Task.CompletedTask;
        }
    }
}