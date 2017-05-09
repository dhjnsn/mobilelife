using System;
using Moq;
using Xunit;

using TaxManager.Models;
using TaxManager.Controllers;

namespace TaxManager.Tests
{
    public class AppliedTaxesControllerTests
    {
        [Fact]
        public void Get_ReturnsBadResult_WhenDateIsInvalid()
        {
            var mockRepository = new Mock<IRepositoryFacade<Municipality>>();

            // The ModelState is not valid when the date can't be parsed
            var ctrl = new AppliedTaxesController(mockRepository.Object);
            ctrl.ModelState.AddModelError("error", "message");

            var response = ctrl.Get("", new DateTime());
            Assert.IsType<Microsoft.AspNetCore.Mvc.
                BadRequestObjectResult>(response);
        }

        [Fact]
        public void Get_ReturnsNotFound_WhenMunicipalityDoesntExist()
        {
            var mockRepository = new Mock<IRepositoryFacade<Municipality>>();
            mockRepository.Setup(r => r.GetByName("berlin"))
                .Returns((Municipality)null);
            var ctrl = new AppliedTaxesController(mockRepository.Object);
            var response = ctrl.Get("berlin", new DateTime());
            Assert.IsType<Microsoft.AspNetCore.Mvc.
                NotFoundObjectResult>(response);
        }

        [Fact]
        public void Get_ReturnsTaxes_AppliedOnProvidedDate()
        {
            var date = DateTime.Parse("2016-01-01");
            Municipality vilnius = new Municipality() { Name = "Vilnius" };
            vilnius.AddScheduledTax(0.2M, date, TimeSpan.Parse("366"));
            var mockRepository = new Mock<IRepositoryFacade<Municipality>>();
            mockRepository.Setup(r => r.GetByName("vilnius"))
                    .Returns(vilnius);

            var ctrl = new AppliedTaxesController(mockRepository.Object);
            var response = ctrl.Get("vilnius", date);

            Assert.IsType<Microsoft.AspNetCore.Mvc.
                OkObjectResult>(response);
            Assert.Equal(0.2M,((Microsoft.AspNetCore.Mvc.
                OkObjectResult)response).Value);
        }
    }
}