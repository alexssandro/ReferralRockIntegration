using ReferralRockIntegration.Web.Models.Enum;
using System.ComponentModel.DataAnnotations;

namespace ReferralRockIntegration.Web.Models
{
    public class ReferralViewModel
    {
        public string? Id { get; set; }
        public string ReferralCode { get; set; }
        public string? MemberId { get; set; }

        [Display(Name = "First name")]
        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; }
        
        [Required(ErrorMessage = "E-mail is required")]
        [EmailAddress(ErrorMessage = "E-mail needs to be in a valid format")]
        public string Email { get; set; }
        public FormAction FormAction { get; set; }
    }
}