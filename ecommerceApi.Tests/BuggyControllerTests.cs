using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ecommerceApi.Controllers;
using Xunit;

namespace ecommerceApiTest.Controllers
{
    public class BuggyControllerTests
    {

        [Fact]
        public void GetUnAuthorised_returnUnauthrizedError()
        {
            var controller = new BuggyController();
            var result = controller.GetUnAuthorised();

            Assert.NotNull( result);
        }

        [Fact]
        public void GetNotFound_returnNotFoundResult()
        {
            var controller = new BuggyController();
            var result = controller.GetNotFound();

            Assert.NotNull(result);
        }
    }
}
