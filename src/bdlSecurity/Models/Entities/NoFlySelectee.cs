using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;




namespace bdlSecurity.Models
{


    [Table("NoFlySelectee")]
    public partial class NoFlySelectee
    {
        [Key]
        [Column(Order = 0)]
        public double SID { get; set; }

        [StringLength(255)]
        public string List { get; set; }

        [StringLength(255)]
        public string CLEARED { get; set; }

        //[Key]
        [Column(Order = 1)]
        [StringLength(255)]
        public string LASTNAME { get; set; }

        [StringLength(255)]
        public string FIRSTNAME { get; set; }

        [StringLength(255)]
        public string MIDDLENAME { get; set; }

        [StringLength(255)]
        public string TYPE { get; set; }

        public DateTime? DOB { get; set; }

        [StringLength(255)]
        public string POB { get; set; }

        [StringLength(255)]
        public string CITIZENSHIP { get; set; }

        [Column("PASSPORT/IDNUMBER")]
        [StringLength(255)]
        public string PASSPORT_IDNUMBER { get; set; }

        [StringLength(255)]
        public string MISC { get; set; }

        public DateTime? VersionDate { get; set; }

        [StringLength(50)]
        public string Version { get; set; }
    }
}
