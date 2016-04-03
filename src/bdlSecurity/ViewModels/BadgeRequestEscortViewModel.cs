using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace bdlSecurity.ViewModels
{
    public class BadgeRequestEscortViewModel
    {

        [StringLength(50)]
        public string RequestorBadgeNumber { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long RequestID { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EscortNo { get; set; }

        [StringLength(25)]
        public string lname { get; set; }

        [StringLength(25)]
        public string fname { get; set; }

        [StringLength(25)]
        public string mname { get; set; }

        [StringLength(100)]
        public string Company { get; set; }

        [StringLength(10)]
        public string BadgeNo { get; set; }

        [StringLength(20)]
        public string Phone { get; set; }
        
    }
}
