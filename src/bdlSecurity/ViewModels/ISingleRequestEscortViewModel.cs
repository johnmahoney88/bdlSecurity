using bdlSecurity.Models;

namespace bdlSecurity.ViewModels
{
    public interface ISingleRequestEscortViewModel
    {
        tb_request_escort Escort { get; set; }
        string ReturnAction { get; set; }
        string RequestAction { get; set; }
    }
}
