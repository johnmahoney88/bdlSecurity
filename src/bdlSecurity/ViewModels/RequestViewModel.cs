using bdlSecurity.Common;
using bdlSecurity.Models;
using Microsoft.AspNet.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace bdlSecurity.ViewModels
{
    public class RequestViewModel : IRequestViewModel
    {
        ISecurityRepository _repository;
        List<tb_request> _requests;
        DataPager<tb_request> _pager;
        private BadgeUserManager _manager;

        public RequestViewModel(ISecurityRepository repository, BadgeUserManager userManager)
        {
            _repository = repository;
            _manager = userManager;
        }

        public List<tb_request> Requests
        {
            get {
                var requestorBadge = GetRequestorBadge();
                if (_requests == null) _requests = _repository.GetAllRequests(requestorBadge);
                return _requests.OrderByDescending(r=> r.RequestID).ToList();
            }
            set { if (_requests!=value) _requests = value; }
        }

        public DataPager<tb_request> Pager
        {
            get
            {
                if (_pager == null)
                {
                    var pagesize = int.Parse(Startup.Configuration["AppConfig:GridPageSize"]);
                    _pager = new DataPager<tb_request>(this.Requests, pagesize);
                }
                return _pager;
            }
        }

        public tb_request GetRequest(long requestID)
        {
            var requestorBadge = GetRequestorBadge();
            return _repository.GetRequestByID(requestorBadge, requestID);
        }
        private string GetRequestorBadge()
        {
            return _manager.User.RequestBadgeNumber;
            var badgeClaim = ClaimsPrincipal.Current.Claims.Where(c => c.Type == Globals.ClaimsTypes.RequestorBadgeNumber).FirstOrDefault();
            if (badgeClaim == null) throw new UnauthorizedAccessException("No Requestor Badge information available.  Request cancelled.");
            return badgeClaim.Value;

        }
    }
}
