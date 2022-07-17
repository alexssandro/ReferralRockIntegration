namespace ReferralRockIntegration.ApiWrapper.Models
{
    public class MemberRequestParameter
    {
        public string? Query { get; set; }
        public bool? ShowDisabled { get; set; }
        public string Sort { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public int? OffSet { get; set; }
        public int? Count { get; set; }
    }
}
