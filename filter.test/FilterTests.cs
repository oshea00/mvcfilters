using filter.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using NUnit.Framework;
using System;

namespace Tests
{
    public class Tests
    {

        [SetUp]
        public void SetUp()
        {
        }

        [Test]
        public void IsTrue()
        {
            Assert.IsTrue(true);
        }

        [Test]
        public void FilterChecksIfNotHttpsRequest()
        {
            //var authContext = Substitute.For<AuthorizationFilterContext>();
            //var filterAttr = new HttpsOnlyAttribute();
            //authContext.HttpContext.Request.IsHttps.Returns(false);
            //filterAttr.OnAuthorization(authContext);
            //var result = authContext.Received().Result;

        }
    }
}