using bdlSecurity.Models;

namespace bdlSecurity.ViewModels
{
    public class PrintRequestViewModel
    {
        public tb_request Request { get; set; }

        public static PrintRequestViewModel CreateFromRequest(tb_request request)
        {
            var vm = new PrintRequestViewModel();
            vm.Request = request;
            return vm;
        }
    }
}
