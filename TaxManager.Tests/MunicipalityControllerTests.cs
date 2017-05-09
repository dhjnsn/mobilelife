using System;
using Moq;
using Xunit;

using TaxManager.Models;
using TaxManager.Controllers;

namespace TaxManager.Tests
{
    public class MunicipalityControllerTests
    {
        [Fact]
        public void Put_UpdatesTaxes_InExistingMunicipality()
        {
            Municipality berlin = new Municipality() { Name = "Berlin" };

            // Setup a mock of the repository facade to return berlin
            var mockRepository = new Mock<IRepositoryFacade<Municipality>>();
            mockRepository.Setup(r => r.GetByName("berlin"))
                    .Returns(berlin);

            var ctrl = new MunicipalitiesController(mockRepository.Object);
            var response = ctrl.Put("berlin", DateTime.Parse("2017-01-01"),
                TimeSpan.Parse("1"), 0.5M);

            Assert.Equal(1, berlin.Taxes.Count);
            Assert.IsType<Microsoft.AspNetCore.Mvc.OkResult>(response);
        }

        [Fact]
        public void Put_CreatesNewMunicipality_WhenNameDoesntExist()
        {
            Municipality berlin = null;

            var mockRepository = new Mock<IRepositoryFacade<Municipality>>();
            // Return null when getting berlin
            mockRepository.Setup(r => r.GetByName("berlin"))
                .Returns((Municipality)null);
            // Setup a callback to give us the new municipality to inspect
            mockRepository.Setup(r => r.Upsert(It.IsAny<Municipality>()))
                    .Callback<Municipality>(m => berlin = m);

            var ctrl = new MunicipalitiesController(mockRepository.Object);
            var response = ctrl.Put("berlin", DateTime.Parse("2017-01-01"),
                TimeSpan.Parse("1"), 0.5M);

            Assert.NotNull(berlin);
            Assert.Equal(0.5, Convert.ToDouble(berlin.Taxes[0].Tax), 2);
            Assert.IsType<Microsoft.AspNetCore.Mvc.OkResult>(response);
        }

        [Fact]
        public void Put_ReturnsBadResult_WhenURLIsWrong()
        {
            var mockRepository = new Mock<IRepositoryFacade<Municipality>>();
            var ctrl = new MunicipalitiesController(mockRepository.Object);
            ctrl.ModelState.AddModelError("error", "message");

            // It doesn't matter what arguements are passed
            var response = ctrl.Put("", new DateTime(), new TimeSpan(), 0M);

            Assert.IsType<Microsoft.AspNetCore.Mvc.
                BadRequestObjectResult>(response);
        }
    }
}