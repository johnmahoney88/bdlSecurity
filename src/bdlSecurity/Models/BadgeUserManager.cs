using bdlSecurity.Common;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace bdlSecurity.Models
{
    public class BadgeUserManager 
        {
        public BadgeUser User { get; set; }
        public BadgeUserManager()
        {        }

        public BadgeUser AuthenticateUser(string username, int pwd)
        {

            using (SecurityContext context = new SecurityContext())
            {
                var badgeUsers = from u in context.tb_user
                                 where u.b_number_str == username && u.b_pin == pwd
                                 select u;

                if (badgeUsers != null && badgeUsers.Count()>0)
                {
                    var badgeUser = badgeUsers.First();
                    var claims = new List<Claim>
                    {
                        new Claim(Globals.ClaimsTypes.UserID, badgeUser.b_number_str),
                        new Claim(Globals.ClaimsTypes.RequestorBadgeNumber, badgeUser.b_number_str)
                    };

                    var id = new ClaimsIdentity(claims, "ApplicationCookie");
                    User = new BadgeUser(new ClaimsPrincipal(id)) { b_number_str = badgeUser.b_number_str, b_design = badgeUser.b_design, b_reason = badgeUser.b_reason };
                    User.IsAuthenticated = true;
                }
            }

            return User;
        }

        public string UserID
        {
            get
            {
                if (this.User == null) return string.Empty;
                return this.User.b_number_str;
            }
        }

        public bool IsUserAuthenticated { get { return (this.User != null && this.User.IsAuthenticated); } }
    }
}
