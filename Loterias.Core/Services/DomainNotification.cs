using Loterias.Core.Dtos;
using Loterias.Core.Interfaces;

namespace Loterias.Core.Services
{
    public class DomainNotification : IDomainNotification
    {
        private readonly List<Notification> _notifications;

        public DomainNotification()
        {
            _notifications = new List<Notification>();
        }

        public bool HasNotifications()
        {
            return _notifications.Count > 0;
        }

        public List<Notification> GetNotifications()
        {
            return _notifications;
        }

        public void AddNotification(Notification notification)
        {
            if (notification == null)
                return;

            _notifications.Add(notification);
        }

        public void AddNotifications(List<Notification> notifications)
        {
            if (notifications == null)
                return;

            _notifications.AddRange(notifications);
        }
    }
}
