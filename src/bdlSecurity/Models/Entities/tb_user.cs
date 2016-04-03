using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bdlSecurity.Models
{

    [Table("tb_user")]
    public partial class tb_user 
    {
        [Key]
        [StringLength(20)]
        public string b_number_str { get; set; }

        public int? b_pin { get; set; }

        public int? b_design { get; set; }

        [StringLength(32)]
        public string b_reason { get; set; }
    }
}
