using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bdlSecurity.Models;

namespace bdlSecurity.ViewModels
{
    public class SingleRequestViewModel : ISingleRequestViewModel
    {
        public tb_request Request { get; set; }
        public string ReturnAction { get; set; }
        public static SingleRequestViewModel CreateFromRequest(tb_request request)
        {
            var vm = new SingleRequestViewModel();
            vm.Request = request;
            return vm;
        }

        
    }
}
