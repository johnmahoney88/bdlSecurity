using Microsoft.VisualStudio.TestTools.UnitTesting;
using bdlSecurity.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace bdlSecurity.Controllers.Tests
{
    [TestClass()]
    public class RequestControllerTests
    {
        [TestMethod()]
        public void IndexTest()
        {
            RequestController controller = new RequestController();

            // Act
            ActionResult result = controller.Index();

            // Assert
            Assert.IsNotNull(result);
        }
    }
}