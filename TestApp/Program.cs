using System;
using TrainingNotificator.Bot;

namespace TestApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var bot = new TelegramBot();
            bot.Start();
            Console.ReadLine();
        }
    }
}