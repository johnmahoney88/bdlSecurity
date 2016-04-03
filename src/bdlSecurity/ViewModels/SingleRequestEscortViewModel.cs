using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bdlSecurity.Models;

namespace bdlSecurity.ViewModels
{
    public class SingleRequestEscortViewModel : ISingleRequestEscortViewModel
    {
        public tb_request_escort Escort { get; set; }
        /// <summary>
        /// Action to return in routing for escorts
        /// </summary>
        public string ReturnAction { get; set; }
        /// <summary>
        /// Current Request (tb_request) action in flight
        /// </summary>
        public string RequestAction { get; set; }

        public static SingleRequestEscortViewModel CreateFromEscort(tb_request_escort escort)
        {
            var vm = new SingleRequestEscortViewModel();
            vm.Escort = escort;
            return vm;
        }

        
    }
}
