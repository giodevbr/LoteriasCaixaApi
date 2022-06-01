using Loterias.Core.Dtos;

namespace Loterias.Core.Interfaces
{
    public interface IDomainNotification
    {
        public bool HasNotifications();
        public List<Notification> GetNotifications();
        public void AddNotification(string notification);
    }
}
