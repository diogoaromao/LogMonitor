using LogMonitor.Domain.Notification.Interfaces;
using LogMonitor.Utils.Logger;

namespace LogMonitor.Domain.Notification
{
    public abstract class Notification : INotification
    {
        protected Printer _printer;

        public Notification()
        {
            _printer = Printer.Instance;
        }

        public abstract void Notify();
    }
}
