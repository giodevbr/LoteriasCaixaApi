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

        public void AddNotification(string notification)
        {
            if (string.IsNullOrEmpty(notification))
                return;

            var newNotification = new Notification(notification);

            AddNotification(newNotification);
        }

        private void AddNotification(Notification notification)
        {
            if (notification == null)
                return;

            _notifications.Add(notification);
        }
    }
}
