
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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

        protected bool ValidOperation()
        {
            return !_notifier.HasNotification();
        }

        protected ActionResult CustomResponse(object? result = null)
        {
            if (ValidOperation())
            {
                return Ok(new
                {
                    success = true,
                    data = result
                });
            }

            return BadRequest(new
            {
                success = false,
                errors = _notifier.GetNotifications()
                                  .Select(n => new { n.Message, n.Field })
            });
        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            if (!modelState.IsValid)
                NotifyErrorInvalidModel(modelState);

            return CustomResponse();
        }

        protected void NotifyErrorInvalidModel(ModelStateDictionary modelState)
        {

            var modelStateValues = modelState.Values.ToList();


            var errors = modelState.Keys
             .SelectMany(key => modelState[key].Errors.Select(x => new { Field = key, ErrorMessage = x.ErrorMessage, Exception = x.Exception })).ToList();


            foreach (var error in errors)
            {
                var errorMsg = error.Exception == null ? error.ErrorMessage : error.Exception.Message;

                NotifyError(errorMsg, error.Field);
            }

        }

        protected void NotifyError(string message, string field = null)
        {
            _notifier.Handle(new Notification(message, field));
        }
    }
}
