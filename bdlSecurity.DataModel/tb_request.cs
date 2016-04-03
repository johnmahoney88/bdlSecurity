using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace bdlSecurity.DataModel
{


    public partial class tb_request
    {
        [Column(Order = 0)]
        [StringLength(50)]
        public string RequestorBadgeNumber { get; set; }

        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long RequestID { get; set; }

        [Column(Order = 2, TypeName = "date")]
        public DateTime RequestDate { get; set; }

        [Column(Order = 3)]
        [StringLength(25)]
        public string lname { get; set; }

        [Column(Order = 4)]
        [StringLength(25)]
        public string fname { get; set; }

        [StringLength(25)]
        public string mname { get; set; }

        [StringLength(100)]
        public string alias { get; set; }

        [Column(Order = 5, TypeName = "date")]
        public DateTime DOB { get; set; }

        [StringLength(10)]
        public string SSN { get; set; }

        [StringLength(10)]
        public string BirthCountry { get; set; }

        [StringLength(100)]
        public string EscortName { get; set; }

        [StringLength(10)]
        public string EscortBadge { get; set; }

        [StringLength(100)]
        public string EscortCompany { get; set; }

        [StringLength(20)]
        public string EscortPhone { get; set; }

        public DateTime? TBStart { get; set; }

        public DateTime? TBEnd { get; set; }

        [StringLength(10)]
        public string TBNumber { get; set; }

        [StringLength(255)]
        public string TBReason { get; set; }

        [StringLength(10)]
        public string Status { get; set; }

        public long? Score { get; set; }

        [StringLength(255)]
        public string Button { get; set; }

        public DateTime? ScoreTS { get; set; }

        public DateTime? CreatedTS { get; set; }

        public DateTime? LastUpdate { get; set; }

        public List<tb_request_escort> RequestEscorts { get; set; }

        [Column(TypeName = "timestamp")]
        [MaxLength(8)]
        [Timestamp]
        public byte[] LastUpdateTS { get; set; }
    }
}
