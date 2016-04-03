using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.Security.Principal;
using System;
using System.Security.Claims;
using bdlSecurity.Common;

namespace bdlSecurity.Models
{
    public class BadgeUser 
    {
        public BadgeUser(ClaimsPrincipal principal)
        {
            this.Principal = principal;
        }
        [Key]
        [StringLength(20)]
        public string b_number_str { get; set; }

        public int? b_pin { get; set; }

        public int? b_design { get; set; }

        [StringLength(32)]
        public string b_reason { get; set; }

        public string Name
        {
            get
            {
                return b_number_str;
            }
        }
        public string AuthenticationType { get; set; }

        public bool IsAuthenticated { get; set; }
        public ClaimsPrincipal Principal { get; private set; }

        public string RequestBadgeNumber
        {
            get
            {
                var badgeClaim = Principal.FindFirst(Globals.ClaimsTypes.RequestorBadgeNumber);
                if (badgeClaim == null) throw new UnauthorizedAccessException("RequestorBadgeNumber is null.  Request access not authorized.");
                return badgeClaim.Value;
            }
        }

}
}
