using ReferralRockIntegration.Service.Interfaces;
using ReferralRockIntegration.Service.Models.Notification;

namespace ReferralRockIntegration.Service
{
    public abstract class BaseService
    {
        protected readonly INotifier _notifier;

        protected BaseService(INotifier notifier)
        {
            _notifier = notifier;
        }

        protected void Notify(string message, string field = null)
        {
            _notifier.Handle(new Notification(message, field));
        }
    }
}
