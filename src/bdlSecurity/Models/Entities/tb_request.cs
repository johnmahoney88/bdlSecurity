using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace bdlSecurity.Models
{


    public partial class tb_request
    {
        private ICollection<tb_request_escort> _escorts;

        [Required]
        [StringLength(50)]
        public string RequestorBadgeNumber { get; set; }

        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long RequestID { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime RequestDate { get; set; }

        [Required]
        [StringLength(25)]
        public string lname { get; set; }

        [Required]
        [StringLength(25)]
        public string fname { get; set; }

        [StringLength(25)]
        public string mname { get; set; }

        [StringLength(100)]
        public string alias { get; set; }

        [StringLength(100)]
        public string Address { get; set; }

        [StringLength(50)]
        public string City { get; set; }

        [StringLength(2)]
        public string State { get; set; }

        [StringLength(10)]
        public string Zipcode { get; set; }

        [StringLength(20)]
        public string Phone { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }

        [StringLength(10)]
        public string SSN { get; set; }

        [StringLength(50)]
        public string BirthCity { get; set; }

        [StringLength(2)]
        public string BirthState { get; set; }

        [StringLength(10)]
        public string BirthCountry { get; set; }

        [StringLength(100)]
        public string CompanyName { get; set; }


        [StringLength(20)]
        public string CompanyPhone { get; set; }

        [DataType(DataType.Date)]
        public DateTime? TBStart { get; set; }

        [DataType(DataType.Date)]
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

        public ICollection<tb_request_escort> RequestEscorts
        {
            get
            {
                if (_escorts == null) _escorts = new Collection<tb_request_escort>();
                return _escorts;
            }
            set
            {
                _escorts = value;
            }
        }
    }

    public partial class tb_request
    {
        public static tb_request CreateRequest(string userID)
        {
            if (string.IsNullOrEmpty(userID)) throw new ArgumentNullException("userID");
            int dobOffset = -1;
            if (int.TryParse(Startup.Configuration["AppConfig:DOBOffSetInYears"], out dobOffset) == false)
                dobOffset = -16;   // default of 20  mins
            var req = new tb_request()
            {
                RequestorBadgeNumber = userID,
                RequestDate = DateTime.Now,
                Score = -1,
                Status = "Pending",
                RequestEscorts = new List<tb_request_escort>(),
                DOB = DateTime.Now.AddYears(dobOffset),
                TBStart = DateTime.Now,
                TBEnd = DateTime.Now
            };
            return req;
        }
    }
}
