using System;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bdlSecurity.Models
{
    public partial class tb_request_escort
    {

        [Required]
        [StringLength(50)]
        public string RequestorBadgeNumber { get; set; }

        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long RequestID { get; set; }

        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EscortNo { get; set; }

        //[Key]
        [Required]
        [StringLength(25)]
        public string lname { get; set; }

        [Required]
        [StringLength(25)]
        public string fname { get; set; }

        [StringLength(25)]
        public string mname { get; set; }

        [Required]
        [StringLength(100)]
        public string Company { get; set; }

        [Required]
        [StringLength(10)]
        public string BadgeNo { get; set; }

        [StringLength(20)]
        public string Phone { get; set; }

        public DateTime? LastUpdate { get; set; }

        public tb_request Request { get; set; }
    }

    public partial class tb_request_escort
    {
        public static tb_request_escort CreateRequestEscort(tb_request req)
        {
            var escort = new tb_request_escort()
            {
                Request = req,
                RequestorBadgeNumber = req.RequestorBadgeNumber,
                EscortNo = (req.RequestEscorts != null) ? (req.RequestEscorts.Count > 0 ? req.RequestEscorts.Max(e => e.EscortNo) + 1 : 1) : 1,
                RequestID = req.RequestID
            };
            return escort;

        }
    }
}
