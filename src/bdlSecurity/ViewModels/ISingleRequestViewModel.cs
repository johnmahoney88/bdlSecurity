using bdlSecurity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bdlSecurity.ViewModels
{
    public interface ISingleRequestViewModel
    {
        tb_request Request { get; }
        string ReturnAction { get; }
    }
}
