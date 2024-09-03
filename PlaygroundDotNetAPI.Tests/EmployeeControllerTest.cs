using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using PlaygroundDotNetAPI.Controllers;

namespace PlaygroundDotNetAPI.Tests
{
    [TestFixture]
    public class PokedexControllerTests
    {
        private PokedexController _controller;

        [SetUp]
        public void Setup()
        {
            //_controller = new PokedexController();
        }

        [Test]
        public void TODO()
        {
            Assert.Warn("Check headers for EVERY call");
            Assert.Warn("Count the items");
            Assert.Warn("Should be 200");
            Assert.Warn("Should be of type Pokemon");

            var _endpoints = new List<(Type, MethodInfo)>(); // All endpoints in my project
            var asm = Assembly.GetExecutingAssembly();
            var cType = typeof(ControllerBase);
            var types = asm.GetTypes().Where(x => x.IsSubclassOf(cType)).ToList();

            foreach (Type t in types)
            {
                var mInfos = t.GetMethods(BindingFlags.Public | BindingFlags.Instance).Where(x => x.DeclaringType.Equals(t)).ToList();
                foreach (MethodInfo mInfo in mInfos) {
                    _endpoints.Add((t, mInfo));
                }
            }

            //var nonAuthEndPoints = _endpoints.Where(x => !x.IsDefined(typeof(AuthorizeAttribute)) && !x.IsDefined(typeof(AllowAnonymousAttribute)));

            //nonAuthEndPoints.Should().BeEmpty();

        }
    }
}
