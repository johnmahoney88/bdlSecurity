using bdlSecurity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace bdlSecurity.ViewModels
{
    public class BadgeRequestViewModel
    {

        [Required]
        [StringLength(50), MinLength(1)]
        public string RequestorBadgeNumber { get; set; }

        [Required]
        public DateTime RequestDate { get; set; } = DateTime.Now;


        [StringLength(10)]
        public string TBNumber { get; set; }

        [StringLength(255)]
        public string TBReason { get; set; }

        [StringLength(10)]
        public string Status { get; set; }

        public long? Score { get; set; }

        public DateTime? LastUpdate { get; set; }

        public IEnumerable<BadgeRequestEscortViewModel> RequestEscorts { get; set; }


    }
}