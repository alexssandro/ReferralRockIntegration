using System.ComponentModel.DataAnnotations;

namespace ReferralRockIntegration.Web.Models
{
    public class ReferralViewModel
    {
        public string? Id { get; set; }
        public string MemberId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "E-mail is required")]
        [EmailAddress(ErrorMessage = "E-mail needs to be in a valid format")]
        public string Email { get; set; }
    }
}
