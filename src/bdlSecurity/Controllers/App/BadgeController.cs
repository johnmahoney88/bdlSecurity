using bdlSecurity.ViewModels;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;

namespace bdlSecurity.Controllers
{
    public class BadgeController : Controller
    {
        IBadgeViewModel _vm;
        
        public BadgeController(IBadgeViewModel vm)
        {
            _vm = vm;
        }

        public IActionResult Index()
        {
            return View(_vm);
        }
    }
}
