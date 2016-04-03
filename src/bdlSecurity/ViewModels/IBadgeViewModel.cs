using bdlSecurity.Models;
using System.Collections.Generic;

namespace bdlSecurity.ViewModels
{
    public interface IBadgeViewModel
    {
        List<tb_badge> Badges { get; set; }
    }
}