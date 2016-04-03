using AutoMapper;
using bdlSecurity.Common;
using bdlSecurity.Models;
using bdlSecurity.ViewModels;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;

namespace bdlSecurity.Controllers.Api
{
    [Route("api/request/{requestid}/escorts")]
    public class BadgeRequestEscortController : Controller
    {
        private ILogger<BadgeRequestEscortController> _logger;
        private BadgeUserManager _manager;
        private ISecurityRepository _repository;

        public BadgeRequestEscortController(ISecurityRepository repository, BadgeUserManager manager, ILogger<BadgeRequestEscortController> logger)
        {
            _repository = repository;
            _manager = manager;
            _logger = logger;

        }
        [HttpGet("")]
        public JsonResult Get(long requestid)
        {
            {
                try
                {
                    var req = _repository.GetRequestByID(_manager.UserID, requestid);
                    //var lst = new List<tb_request_escort>()
                    //{ new tb_request_escort() { RequestorBadgeNumber = _manager.UserID, RequestID = requestid },
                    // new tb_request_escort() { RequestorBadgeNumber = _manager.UserID, RequestID = requestid } };

                    if (req == null)
                    {
                        return Json(null);
                    }
                    else
                    {

                        var vm = Mapper.Map<List<BadgeRequestEscortViewModel>>(req.RequestEscorts);

                        return Json(vm);
                    }
                }
                catch (Exception e)
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    var apierr = ResultError.CreateFromException("Error getting escorts for request " + requestid, HttpStatusCode.BadRequest, e);
                    _logger.LogError(apierr.ToString());
                    return Json(apierr);
                }
            }
        }
    }
}
