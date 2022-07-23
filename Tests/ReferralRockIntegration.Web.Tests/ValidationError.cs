using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace ReferralRockIntegration.Web.Tests
{
    public static class ValidationError
    {
        public static void AddValidationErrors(this ModelStateDictionary modelState, object model)
        {
            var context = new ValidationContext(model, null, null);

            var results = new List<ValidationResult>();

            Validator.TryValidateObject(model, context, results, true);
            
            foreach (var result in results)
            {
                var name = result.MemberNames.First();

                modelState.AddModelError(name, result.ErrorMessage);
            }
        }
    }
}
