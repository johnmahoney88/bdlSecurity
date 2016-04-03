using System;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace bdlSecurity.Models
{


    public partial class tb_badge
    {
        public int? c_partition { get; set; }

        public short? c_public { get; set; }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int c_id { get; set; }

        [StringLength(25)]
        public string c_lname { get; set; }

        [StringLength(25)]
        public string c_mname { get; set; }

        [StringLength(25)]
        public string c_fname { get; set; }

        [StringLength(25)]
        public string c_nick_name { get; set; }

        [StringLength(64)]
        public string c_addr { get; set; }

        [StringLength(32)]
        public string c_addr1 { get; set; }

        [StringLength(32)]
        public string c_addr2 { get; set; }

        [StringLength(32)]
        public string c_addr3 { get; set; }

        [StringLength(16)]
        public string c_phone { get; set; }

        [StringLength(6)]
        public string c_ext { get; set; }

        public DateTime? c_s_timestamp { get; set; }

        public DateTime? c_t_timestamp { get; set; }

        public short? c_card_type { get; set; }

        public int? c_dept_id { get; set; }

        public int? c_company_id { get; set; }

        public int? c_sponsor_id { get; set; }

        public short? c_guard { get; set; }

        [StringLength(32)]
        public string c_suite { get; set; }

        [StringLength(32)]
        public string site { get; set; }

        public Guid? c_guid { get; set; }

        public int? c_checksum { get; set; }

        [StringLength(64)]
        public string c_email { get; set; }

        //[Key]
        [Column(Order = 1)]
        [StringLength(20)]
        public string b_number_str { get; set; }

        //[Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int company_id { get; set; }

        [StringLength(32)]
        public string company_name { get; set; }

        [StringLength(32)]
        public string b_reason { get; set; }

        [StringLength(25)]
        public string bl_name { get; set; }

        public int? b_design { get; set; }

        public DateTime? b_exp_timestamp { get; set; }

        public DateTime? b_start_timestamp { get; set; }

        public DateTime? DOB { get; set; }

        [StringLength(10)]
        public string SSN { get; set; }

        [StringLength(255)]
        public string Alias1 { get; set; }

        [StringLength(255)]
        public string Alias2 { get; set; }

        [StringLength(255)]
        public string Alias3 { get; set; }
    }
}
