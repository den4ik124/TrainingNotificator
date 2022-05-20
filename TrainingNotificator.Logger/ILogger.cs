using System;

namespace TrainingNotificator.Logger
{
    public interface ILogger
    {
        void Log(Exception exception);
    }
}