using bdlSecurity.Models;
using System.Collections.Generic;
using System.Security.Principal;

namespace bdlSecurity.ViewModels
{
    public interface IRequestViewModel
    {
        List<tb_request> Requests { get; set; }

        DataPager<tb_request> Pager { get; }

        tb_request GetRequest(long requestID);

    }
}