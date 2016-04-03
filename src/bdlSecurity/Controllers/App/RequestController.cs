using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using bdlSecurity.Models;
using bdlSecurity.ViewModels;
using Microsoft.AspNet.Authorization;
using System.Net;
using bdlSecurity.Common;
using Microsoft.AspNet.Routing;
using System.Diagnostics;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace bdlSecurity.Controllers
{

    public class RequestController : Controller
    {


        IRequestViewModel _vm;
        ISecurityRepository _repository;
        private BadgeUserManager _manager;

        public RequestController(IRequestViewModel vm, ISecurityRepository repository, BadgeUserManager manager)
        {
            Debug.WriteLine("RequestController ctor");

            _vm = vm;
            _repository = repository;
            _manager = manager;
        }

        public IActionResult Index(int? page, string message)
        {
            if (page.HasValue)
            {
                _vm.Pager.SetPageNum(page.Value);
                ViewBag.CurrentPage = page.Value;
                ViewBag.Page = page.Value;
            }
            else if (ViewBag.Page != null)
            { _vm.Pager.SetPageNum(ViewBag.Page); }
            else
            { ViewBag.Page = 1; }
            if (string.IsNullOrEmpty(message) == false)
                ViewBag.Message = message;
            return View(_vm);
        }

        [HttpGet]
        public IActionResult Edit(long RequestID, int page, string message)
        {
            try
            {
                ViewBag.Page = page;
                tb_request req = _vm.GetRequest(RequestID);
                if (req.Score == 0)
                {
                    ModelState.AddModelError("", "Previously verified request cannot be edited.");
                    return RedirectToAction(Globals.RouteActions.Index, GetRouteValues(RequestID, page));
                }
                var vm = SingleRequestViewModel.CreateFromRequest(req);
                vm.ReturnAction = Globals.RouteActions.EditRequest;
                if (req == null) return HttpNotFound();
                if (string.IsNullOrEmpty(message) == false)
                    ViewBag.Message = message;
                return View(vm);
            }
            catch (Exception e)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", ExceptionHelper.ExceptionMessageToString(e));
                return View();
            }
        }

        [HttpPost]
        public IActionResult Edit(SingleRequestViewModel vm, string Command, long requestid, int page)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Page = page;
                // reset score from edit
                vm.Request.Score = -1;
                try
                {
                    if (Command.Equals("Escorts", StringComparison.CurrentCultureIgnoreCase))
                    {
                        _repository.SaveRequest(vm.Request);
                        return Escorts(vm, vm.Request.RequestID, page, "Edit");
                    }
                    else
                    {
                        _repository.SaveRequest(vm.Request);
                        var req = _repository.GetRequestByID(GetRequestorBadge(), vm.Request.RequestID);
                        if (req.RequestEscorts.Count() == 0)
                        {
                            ModelState.AddModelError("", "Request must have at least one escort");
                            return View(vm);
                        }
                        return RedirectToAction(Globals.RouteActions.Index, page);
                    }
                }
                catch (Exception e)
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    ModelState.AddModelError("", ExceptionHelper.ExceptionMessageToString(e));
                    return View(vm);
                }
            }
            HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            ModelState.AddModelError("", ValidationHelper.ValidationMessageToString(ModelState));
            return View();
        }

        public IActionResult Add()
        {
            var req = tb_request.CreateRequest(GetRequestorBadge());
            var vm = SingleRequestViewModel.CreateFromRequest(req);
            vm.ReturnAction = Globals.RouteActions.AddRequest;
            return View(vm);
        }

        [HttpPost]
        public IActionResult Add(SingleRequestViewModel vm, string Command, int page)
        {
            if (ModelState.IsValid)
            {
                try
                {                     
                    if (Command.Equals(Globals.RouteActions.Escorts, StringComparison.CurrentCultureIgnoreCase))
                    {
                        _repository.AddRequest(vm.Request);
                        return Escorts(vm, vm.Request.RequestID, page, vm.ReturnAction);
                    }
                    else 
                    {
                        if (vm.Request.RequestEscorts.Count() == 0)
                        {
                            ModelState.AddModelError("", "Request must have at least one escort");
                            return View(vm);
                        }
                        _repository.SaveRequest(vm.Request);
                        return RedirectToAction(Globals.RouteActions.Index, "Request");
                    }

                }
                catch (Exception e)
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    ModelState.AddModelError("", ExceptionHelper.ExceptionMessageToString(e));
                    return View(vm);
                }
            }
            HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            ModelState.AddModelError("", ValidationHelper.ValidationMessageToString(ModelState));
            return View(vm);
        }

        [HttpGet()]
        public IActionResult Cancel(long RequestID, int page)
        {
            if (RequestID > 0)
            {
                try {
                    var req = _repository.GetRequestByID(GetRequestorBadge(), RequestID);
                    _repository.RemoveRequest(req);
                }
                catch { }   // swallow error for now
            }
            return RedirectToAction(Globals.RouteActions.Index, "Request");
        }

        [HttpGet()]
        public IActionResult Escorts(SingleRequestViewModel vm, long RequestID, int page, string returnAction)
        {

            ViewBag.Page = page;
            ViewBag.ReturnAction = returnAction;
            vm.Request = _repository.GetRequestByID(GetRequestorBadge(), RequestID);
            if (string.IsNullOrEmpty(returnAction) == false)
                vm.ReturnAction = returnAction;           
            if (vm.Request.RequestEscorts.Count == 0)
            {
                return RedirectToAction(Globals.RouteActions.AddEscort, GetRouteValues(RequestID, page, returnAction));
            }
            else
            {
                return View("Escorts", vm);
            }
        }

        [HttpGet]
        public IActionResult EditEscort(long requestid, int escortno, int page, string returnAction)
        {
            var req = _repository.GetRequestByID(GetRequestorBadge(), requestid);
            var vm = SingleRequestEscortViewModel.CreateFromEscort(req.RequestEscorts.Where(e => e.EscortNo == escortno).First());
            vm.RequestAction = returnAction;    // preserve Request workflow
            vm.ReturnAction = Globals.RouteActions.Escorts;
            ViewBag.Page = page;
            if (vm != null)
            {
                return View(vm);
            }
            else
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", "Escort not found.");
                return View(vm);

            }
        }

        [HttpPost]
        public IActionResult EditEscort(SingleRequestEscortViewModel vm, long RequestID, int page)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var escort = vm.Escort;
                    if (_repository.SaveEscort(escort))
                    {
                        var req = _repository.GetRequestByID(GetRequestorBadge(), RequestID);
                        var reqvm = SingleRequestViewModel.CreateFromRequest(req);
                        //return Escorts(reqvm, RequestID, page, vm.RequestAction);
                        return RedirectToAction(Globals.RouteActions.Escorts, GetRouteValues(RequestID, page, vm.RequestAction));
                    }
                    else
                    {
                        ModelState.AddModelError("", "Unable to save escort");
                        return View(escort);
                    }

                }
                catch (Exception e)
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    ModelState.AddModelError("", ExceptionHelper.ExceptionMessageToString(e));
                    return View(vm);
                }
            }
            HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            ModelState.AddModelError("", ValidationHelper.ValidationMessageToString(ModelState));
            return View(vm);
        }

        [HttpGet()]
        public IActionResult AddEscort(long RequestID, int page, string returnAction)
        {
            var req = _repository.GetRequestByID(GetRequestorBadge(), RequestID);
            var escort = tb_request_escort.CreateRequestEscort(req);
            var vm = SingleRequestEscortViewModel.CreateFromEscort(escort);
            vm.RequestAction = returnAction;
            vm.ReturnAction = Globals.RouteActions.Escorts;
            return View(vm);
        }


        [HttpPost()]
        public IActionResult AddEscort(SingleRequestEscortViewModel vm, long RequestID, int page)
        {
            if (ModelState.IsValid)
            {
                var escort = vm.Escort;
                try
                {
                    _repository.AddEscort(escort);
                    return RedirectToAction(Globals.RouteActions.Escorts, GetRouteValues(RequestID, page, vm.RequestAction));
                }
                catch (Exception e)
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    ModelState.AddModelError("", ExceptionHelper.ExceptionMessageToString(e));
                    return View(escort);
                }
            }
            HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            ModelState.AddModelError("", ValidationHelper.ValidationMessageToString(ModelState));
            return View(vm.Escort);

        }


        [HttpGet]
        public IActionResult Verify(long RequestID, int page, string returnAction)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _repository.VerifyRequest(RequestID);
                    var req = _repository.GetRequestByID(GetRequestorBadge(), RequestID);
                    var score = req.Score;
                    string message;
                    if (score==0)
                    {
                        message= "Your request has passed validation.  Please Print Temp Badge Permit.";
                    }
                    else
                    {
                        message = "Your request has not passed validation.  Please contact BDL Security.";
                    }
                    ViewBag .Message = message;

                    if (string.IsNullOrEmpty(returnAction) == false)
                    {
                        return RedirectToAction(returnAction, GetRouteValues(RequestID, page, message));
                    }
                    else
                    {
                        return RedirectToAction("Index", GetRouteValues(page, message));
                    }
                }
                catch (Exception e)
                {
                    // TODO need error handling from controller
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    ModelState.AddModelError("", ExceptionHelper.ExceptionMessageToString(e));
                    ViewBag.Message = "Error:  " + ExceptionHelper.ExceptionMessageToString(e);
                    return new HttpStatusCodeResult((int)HttpStatusCode.BadRequest);
                }
            }
            HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            ModelState.AddModelError("", ValidationHelper.ValidationMessageToString(ModelState));
            if (string.IsNullOrEmpty(returnAction) == false)
            {
                return RedirectToAction(returnAction, GetRouteValues(RequestID, page));
            }
            else
            {
                return RedirectToAction(Globals.RouteActions.Index, page);
            }
        }

        public IActionResult Print(long RequestID)
        {

            try
            {
                ViewBag.HideBannerImage = "hidden";
                tb_request req = _vm.GetRequest(RequestID);
                if (req.Score != 0)
                {
                    ModelState.AddModelError("", "Request not verified.  Badge cannot be printed.");
                    return RedirectToAction(Globals.RouteActions.Index, RequestID);
                }
                var vm = PrintRequestViewModel.CreateFromRequest(req);
                if (req == null) return HttpNotFound();
                return View(vm);
            }
            catch (Exception e)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", ExceptionHelper.ExceptionMessageToString(e));
                return View();
            }
        }

        private string GetRequestorBadge()
        {
            
            var badgeClaim = HttpContext.User.Claims.Where(c => c.Type == Globals.ClaimsTypes.RequestorBadgeNumber).FirstOrDefault();
            if (badgeClaim == null) throw new UnauthorizedAccessException("No Requestor Badge information available.  Request cancelled.");
            return badgeClaim.Value;

        }

        private RouteValueDictionary GetRouteValues(params object[] parms)
        {
            var values = new RouteValueDictionary();
            foreach (var parm in parms)
            {
                if (parm == null) continue;
                if (parm.GetType() == typeof(long))
                {
                    values.Add("RequestID", parm);
                }
                else if (parm.GetType() == typeof(int))
                {
                    values.Add("page", parm);
                }
                else if (parm.GetType() == typeof(string))
                {
                    // cheating on length of string to be message over action
                    if (parm.ToString().Length > 10)
                        values.Add("message", parm);
                    else
                        values.Add("returnAction", parm);
                }
            }
            return values;  //{ { "RequestID", requestid }, { "page", page } };
        }

    }
}
