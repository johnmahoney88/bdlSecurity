using System.Collections.Generic;

namespace bdlSecurity.Models
{
    public interface ISecurityRepository
    {
        List<tb_request> GetAllRequests(string userid);

        tb_request GetRequestByID(string userid, long requestid);

        List<tb_badge> GetUserBadges(string userid);

        List<NoFlySelectee> GetAllNoFlySelectees();

        void VerifyRequest(long requestid);

        bool SaveChanges();
        bool SaveRequest(tb_request req);

        bool SaveEscort(tb_request_escort escort);

        void AddRequest(tb_request req);

        void AddEscort(tb_request_escort escort);
        void RemoveRequest(tb_request request);


        //void Add<T>(T entity) where T : class;

        //void Delete<T>(T entity) where T : class;

        //void Update<T>(T entity) where T : class;


    }

}