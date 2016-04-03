using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bdlSecurity.Common
{
    public abstract class Globals
    {
        public abstract class ClaimsTypes
        {
            public static readonly string UserID = "UserID";
            public static readonly string RequestorBadgeNumber = "RequestorBadgeNumber";
        }

        public abstract class RouteActions
        {
            public static readonly string Index = "Index";
            public static readonly string AddRequest = "Add";
            public static readonly string EditRequest = "Edit";
            public static readonly string Escorts = "Escorts";
            public static readonly string AddEscort = "AddEscort";
            public static readonly string EditEscort = "EditEscort";
        }
    }
}
