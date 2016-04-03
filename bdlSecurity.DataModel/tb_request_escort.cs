using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bdlSecurity.DataModel
{
    public class tb_request_escort
    {

        //[Key]
        [Column(Order = 0)]
        [StringLength(50)]
        public string RequestorBadgeNumber { get; set; }

        //[Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long RequestID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EscortNo{ get; set; }

        //[Key]
        [Column(Order = 3)]
        [StringLength(25)]
        public string lname { get; set; }

        //[Key]
        [Column(Order = 4)]
        [StringLength(25)]
        public string fname { get; set; }

        [StringLength(25)]
        public string mname { get; set; }

        [StringLength(100)]
        public string Company { get; set; }

        [StringLength(10)]
        public string BadgeNo{ get; set; }
                
        [StringLength(20)]
        public string Phone { get; set; }
        
        public DateTime? LastUpdate { get; set; }

        ////[Key]
        //[Column(TypeName = "timestamp")]
        //[MaxLength(8)]
        //[Timestamp]
        //public byte[] LastUpdateTS { get; set; }

        public tb_request Request { get; set; }
    }
}
