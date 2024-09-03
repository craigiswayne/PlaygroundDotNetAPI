using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moq;
using PlaygroundDotNetAPI.Controllers;

namespace PlaygroundDotNetAPI.Tests
{
    [TestFixture]
    public class EmployeeControllerTests
    {
        private EmployeesController _controller;

        [SetUp]
        public void Setup()
        {
            //_controller = new EmployeesController();
        }

        [Test]
        public void TODO()
        {
            Assert.Warn("Check headers for EVERY call");
            Assert.Warn("Count the items");
            Assert.Warn("Should be 200");
            Assert.Warn("Should be of type Employee");
        }
    }
}
