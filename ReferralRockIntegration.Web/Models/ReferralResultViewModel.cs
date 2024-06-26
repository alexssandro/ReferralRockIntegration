﻿using ReferralRockIntegration.Web.Models.Enum;

namespace ReferralRockIntegration.Web.Models
{
    public class ReferralResultViewModel
    {
        public string MemberId { get; set; }
        public string ReferralCode { get; set; }
        public string MemberName { get; set; }
        public string ReferralName { get; set; }
        public FormAction FormAction { get; set; }
    }
}
