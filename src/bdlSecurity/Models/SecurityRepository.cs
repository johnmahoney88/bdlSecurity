using Microsoft.Data.Entity;
using Microsoft.Data.Entity.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bdlSecurity.Models
{
    public class SecurityRepository : ISecurityRepository,IDisposable
    {
        private SecurityContext _context;
        public SecurityRepository(SecurityContext context)
        {
            _context = context;
        }

        public List<tb_request> GetAllRequests(string userid)
        {
            var requests = (from req in _context.tb_request.Include(r => r.RequestEscorts)
                           where req.RequestorBadgeNumber == userid
                           orderby req.RequestDate descending
                           select req).ToList();

            //var escorts = (from re in _context.tb_request_escort
            //               where re.RequestorBadgeNumber == userid
            //               select re).ToList();
            //requests.ForEach(r => r.RequestEscorts = escorts.Where(e => e.RequestID == r.RequestID).ToList());

            return requests.ToList();
        }

        public tb_request GetRequestByID(string userid, long requestID)
        {
            var request = (from req in _context.tb_request.Include(r => r.RequestEscorts)
                           where req.RequestorBadgeNumber == userid && req.RequestID == requestID
                           orderby req.RequestDate descending
                           select req).FirstOrDefault();

            return request;
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

        public void AddRequest(tb_request req)
        {
            _context.Add(req);
            _context.SaveChanges();

            return;
        }

        public void AddEscort(tb_request_escort escort)
        {
            _context.Add(escort);
            _context.SaveChanges();

            return;
        }

        //public void Add<T>(T entity) where T : class
        //{
        //    _context.Add<T>(entity);
        //    _context.SaveChanges();

        //    return;
        //}

        //public void Delete<T>(T entity) where T : class
        //{
        //    _context.Remove<T>(entity);
        //    _context.SaveChanges();

        //    return;
        //}

        //public void Update<T>(T entity) where T : class
        //{
        //    _context.Update<T>(entity);
        //    _context.SaveChanges();

        //    return;
        //}


        public void VerifyRequest(long requestid)
        {
            _context.Database.ExecuteSqlCommand("exec TBcheck " + requestid);
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() > 0;

        }

        public bool SaveRequest(tb_request req)
        {
            _context.Attach(req);
            _context.Entry(req).State = (req.RequestID == 0) ? EntityState.Added : ((_context.Entry(req).State != EntityState.Deleted) ? EntityState.Modified : _context.Entry(req).State);
            
            return (_context.SaveChanges() > 0);
        }

        public bool SaveEscort(tb_request_escort escort)
        {
            _context.Attach(escort);
            _context.Entry(escort).State = (escort.RequestID == 0) ? EntityState.Added : ((_context.Entry(escort).State != EntityState.Deleted) ? EntityState.Modified : _context.Entry(escort).State);

            return (_context.SaveChanges() > 0);
        }

        public void RemoveRequest(tb_request request)
        {
            _context.Remove(request);
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
