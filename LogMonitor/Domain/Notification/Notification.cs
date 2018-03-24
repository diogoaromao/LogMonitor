using LogMonitor.Domain.Notification.Interfaces;

namespace LogMonitor.Domain.Notification
{
    public abstract class Notification : INotification
    {
        public abstract void Notify();
    }
}
