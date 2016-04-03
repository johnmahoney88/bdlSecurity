using bdlSecurity.Models;
using Microsoft.AspNet.Http;
using System.Collections.Generic;

namespace bdlSecurity.ViewModels
{
    public class BadgeViewModel : IBadgeViewModel
    {
        ISecurityRepository _repository;
        BadgeUserManager _manager;

        List<tb_badge> _badges;
        public BadgeViewModel(ISecurityRepository repository, BadgeUserManager userManager)
        {
            _repository = repository;
            _manager = userManager;
        }

        public List<tb_badge> Badges
        {
            get
            {
                if (_manager.User==null)
                    return null;
                    
                if (_badges == null) _badges = _repository.GetUserBadges(_manager.User.b_number_str);
                return _badges;
            }
            set { if (_badges != value) _badges = value; }
        }
    }
}
