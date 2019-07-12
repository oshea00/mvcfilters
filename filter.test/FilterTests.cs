using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using NUnit.Framework;
using NSubstitute;
using filter.Infrastructure;

namespace FIlterTests
{
    public class Filters
    {
        [Test]
        public void NotHttpsChecksRequest()
        {
            // Arrange
            var authContext = Substitute.For<AuthorizationFilterContext>(
                    Substitute.For<ActionContext>(
                        Substitute.For<HttpContext>(),
                        Substitute.For<RouteData>(),
                        Substitute.For<ActionDescriptor>()
                    ),
                    new List<IFilterMetadata>()
               );
            authContext.HttpContext.Request.IsHttps.Returns(false);
            // Act
            var filterAttr = new HttpsOnlyAttribute();
            filterAttr.OnAuthorization(authContext);
            // Assert
            var result = authContext.Result as StatusCodeResult;
            Assert.AreEqual(StatusCodes.Status403Forbidden, result.StatusCode);
        }

    }
}