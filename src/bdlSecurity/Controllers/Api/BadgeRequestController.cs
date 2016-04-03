using AutoMapper;
using bdlSecurity.Common;
using bdlSecurity.Models;
using bdlSecurity.ViewModels;
using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Net;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace bdlSecurity.Controllers.Api
{

    [Route("api/request")]
    public class BadgeRequestController : Controller
    {
        BadgeUserManager _manager;
        ISecurityRepository _repository;

        public BadgeRequestController(ISecurityRepository repository, BadgeUserManager manager)
        {
            _manager = manager;  
            _repository = repository;
        }

        [HttpGet("")]
        public JsonResult GetRequest()
        {
            try
            {
                var reqs = _repository.GetAllRequests(_manager.UserID);
                //var req = new tb_request() { RequestorBadgeNumber = _manager.UserID, RequestDate = DateTime.Now };
                //req.RequestEscorts = new List<tb_request_escort>() { new tb_request_escort() { lname = "Escort1" }, new tb_request_escort() { lname = "Escort2" } };
                //var reqs = new List<tb_request>() { req };
                //reqs.Add(req);
                var vm = Mapper.Map<IEnumerable<BadgeRequestViewModel>>(reqs);
                return Json(vm);
            }
            catch (Exception e)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(ResultError.CreateFromException("Error occurred finding requests for user " + _manager.UserID, HttpStatusCode.BadRequest, e));
            }
        }

        [HttpPost("")]
        public JsonResult AddRequest([FromBody]BadgeRequestViewModel vm)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    // translate ViewModel to entity type
                    var req = Mapper.Map<tb_request>(vm);
                    _repository.SaveRequest(req);
                    // valid request creation
                    Response.StatusCode = (int)HttpStatusCode.Created;

                    return Json(Mapper.Map<BadgeRequestViewModel>(req));
                }
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(ResultError.CreateFromException("Error adding new request", HttpStatusCode.BadRequest, ex));
            }
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json(ResultError.CreateFromModelState(HttpStatusCode.BadRequest, ModelState));
        }

        [HttpGet("{requestid}")]
        public JsonResult GetRequest(long requestid)
        {
            try
            {
                var req = _repository.GetRequestByID(_manager.UserID, requestid);
                var vm = Mapper.Map<BadgeRequestViewModel>(req);
                return Json(vm);
            }
            catch (Exception e)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json("Error occurred finding requests " + requestid + " Error:  " + e.Message);
            }
        }


        [HttpPost("verify/{requestid}")]
        public JsonResult VerifyRequest(long requestid)
        {
            try
            {
                _repository.VerifyRequest(requestid);
                var req = _repository.GetRequestByID(_manager.UserID, requestid);
                return Json(req.Score);
            }
            catch (Exception e)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(ResultError.CreateFromException("Error verifying request " + requestid, HttpStatusCode.BadRequest, e));
            }
        }
            

        }
    }
