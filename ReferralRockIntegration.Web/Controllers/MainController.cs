﻿
using Microsoft.AspNetCore.Mvc;
using ReferralRockIntegration.Service.Interfaces;
using ReferralRockIntegration.Service.Models.Notification;

namespace ReferralRockIntegration.Web.Controllers
{
    public class MainController : Controller
    {
        private readonly INotifier _notifier;

        public MainController(INotifier notifier)
        {
            _notifier = notifier;
        }

        protected bool IsThereAnyError()
        {
            bool hasNotifications = _notifier.HasNotification();

            if (!hasNotifications)
                return false;

            foreach (var item in _notifier.GetNotifications())
            {
                NotifyError(item.Message, item.Field);
            }

            return true;
        }

        protected void NotifyError(string message, string field = null)
        {
            ModelState.AddModelError(field ?? "", message);
        }

        protected bool HasNotification()
        {
            return _notifier.HasNotification();
        }

        protected List<Notification> GetNotifications()
        {
            return _notifier.GetNotifications();
        }
    }
}
