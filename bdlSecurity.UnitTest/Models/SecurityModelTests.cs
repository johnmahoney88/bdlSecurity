using Microsoft.VisualStudio.TestTools.UnitTesting;
using bdlSecurity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bdlSecurity.Models.Tests
{
    [TestClass()]
    public class SecurityModelTests
    {
        [TestMethod()]
        public void SecurityModelNoFlySelecteeTest()
        {
            var model = new SecurityModel();
            var first = model.NoFlySelectee.FirstOrDefault();
            Assert.IsNotNull(first);
        }

        [TestMethod()]
        public void SecurityModelbadgeTest()
        {
            var model = new SecurityModel();
            var first = model.tb_badge.FirstOrDefault();
            Assert.IsNotNull(first);
        }

        [TestMethod()]
        public void SecurityModelrequestTest()
        {
            var model = new SecurityModel();
            var first = model.tb_request.FirstOrDefault();
            Assert.IsNotNull(first);
        }

        [TestMethod()]
        public void SecurityModeluserTest()
        {
            var model = new SecurityModel();
            var first = model.tb_user.FirstOrDefault();
            Assert.IsNotNull(first);
        }
    }
}