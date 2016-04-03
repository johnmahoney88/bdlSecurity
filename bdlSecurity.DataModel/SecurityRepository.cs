using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bdlSecurity.DataModel
{
    public class SecurityRepository : ISecurityRepository
    {
        private SecurityContext _context;
        public SecurityRepository(SecurityContext context)
        {
            _context = context;
        }

        public List<tb_request> GetAllRequests(string userid)
        {
            var requests = from req in _context.tb_request.Include(r => r.RequestEscorts)
                           where req.RequestorBadgeNumber == userid
                           orderby req.RequestDate descending
                           select req;

            //var escorts = (from re in _context.tb_request_escort
            //               where re.RequestorBadgeNumber == userid
            //               select re).ToList();
            //requests.ForEach(r => r.RequestEscorts = escorts.Where(e => e.RequestID == r.RequestID).ToList());

            return requests.ToList();
        }

        public List<tb_badge> GetUserBadges(string userid)
        {
            var badges = from tb in _context.tb_badge
                         where tb.b_number_str == userid
                         orderby tb.c_id descending
                         select tb;
            return badges.ToList();
        }

        public List<NoFlySelectee> GetAllNoFlySelectees()
        {
            return _context.NoFlySelectee.OrderByDescending(n => n.SID).ToList();
        }
    }
}
