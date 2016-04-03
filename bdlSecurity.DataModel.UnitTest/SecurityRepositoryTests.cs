using Microsoft.VisualStudio.TestTools.UnitTesting;
using bdlSecurity.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bdlSecurity.DataModel.Tests
{
    [TestClass()]
    public class SecurityRepositoryTests
    {
        public const string connection = "Server=tb.ctair.us;Database=Security;User=ssa;Password=ldb$8ldb;MultipleActiveResultSets=True;App=bdlSecurity Web Application;Connection Timeout=15;";
        public string userid = "53788";
        [TestMethod()]
        public void GetAllRequestsTest()
        {
            var resp = new SecurityRepository(new SecurityContext(connection));
            resp.GetAllRequests(userid);


        }
    }
}