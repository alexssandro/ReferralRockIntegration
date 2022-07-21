using ReferralRockIntegration.Service.Models.Notification;

namespace ReferralRockIntegration.Service.Interfaces
{
    public interface INotifier
    {
        bool HasNotification();
        List<Notification> GetNotifications();
        void Handle(Notification notification);
    }
}
