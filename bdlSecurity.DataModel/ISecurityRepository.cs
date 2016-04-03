using System.Collections.Generic;

namespace bdlSecurity.DataModel
{
    public interface ISecurityRepository
    {
        List<tb_request> GetAllRequests(string userid);

        List<tb_badge> GetUserBadges(string userid);

        List<NoFlySelectee> GetAllNoFlySelectees();
    }

}