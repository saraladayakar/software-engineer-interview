
using FluentAssertions;
using InstallmentServices.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Net;
using Zip.InstallmentsService;
using Zip.InstallmentsService.Model;

namespace InstallmentServiceUnit
{
    [TestClass]
    public class InstallServicecontrollerTests
    {

        InstallmentServiceController controller;
        Mock<IPlaymentPlanFactory> mockFactory;
        InstallServiceRequest req;
        Mock<ILogger> _log;


       [TestInitialize]
        public void Setup()
        {
           
            req = new InstallServiceRequest
            {
                NoOfInstallment = 4,
                Amount = 1000,
                DateofOrder = System.DateTime.Now,
                Frequencty = 15
            };
            _log = new Mock<ILogger>();
            mockFactory = new Mock<IPlaymentPlanFactory>();
            controller = new InstallmentServiceController(mockFactory.Object, _log.Object);

            mockFactory = new Mock<IPlaymentPlanFactory>();
           // mockupdateBL.Setup(m => m.Update(It.IsAny<UpdateRequest>())).Returns(new List<string>());

            mockFactory.Setup(x => x.CreatePaymentPlan(It.IsAny<InstallServiceRequest>())).Returns((PaymentPlan)It.IsAny<object>());
                

        }
        [TestCleanup]
        public void cleanup() {
        
        }


        [TestMethod]
        public void ReturnBadRequestWhenRequestNull()
        {
            req = null;
             var actionResult = controller.InstallmentService(req);
            actionResult.Should().BeOfType<ObjectResult>();

            var statusCode = (HttpStatusCode)((ObjectResult)actionResult).StatusCode;
            statusCode.Should().Be(HttpStatusCode.BadRequest);

            var actualResponse = (ErrorResponse)((ObjectResult)actionResult).Value;
            actualResponse.Errors.Count.Should().Be(1);
            actualResponse.Errors[0].title.Should().Be("Request is null");


        }

        [TestMethod]
        public void ReturnBadRequestWhenAmountZero()
        {
            req.Amount =0;
            var actionResult = controller.InstallmentService(req);
            actionResult.Should().BeOfType<ObjectResult>();

            var statusCode = (HttpStatusCode)((ObjectResult)actionResult).StatusCode;
            statusCode.Should().Be(HttpStatusCode.BadRequest);

            var actualResponse = (ErrorResponse)((ObjectResult)actionResult).Value;
            actualResponse.Errors.Count.Should().Be(1);
            actualResponse.Errors[0].title.Should().Be("Invalid Amount is entered");
        }

        [TestMethod]
        public void ReturnBadRequestWhenAmountLessThenZero()
        {
            req.Amount = -100;
            var actionResult = controller.InstallmentService(req);
            actionResult.Should().BeOfType<ObjectResult>();

            var statusCode = (HttpStatusCode)((ObjectResult)actionResult).StatusCode;
            statusCode.Should().Be(HttpStatusCode.BadRequest);

            var actualResponse = (ErrorResponse)((ObjectResult)actionResult).Value;
            actualResponse.Errors.Count.Should().Be(1);
            actualResponse.Errors[0].title.Should().Be("Invalid Amount is entered");
        }



        [TestMethod]
        public void ReturnBadRequestWhenFrequencyyZero()
        {
            req.Frequencty =0;
            var actionResult = controller.InstallmentService(req);
            actionResult.Should().BeOfType<ObjectResult>();

            var statusCode = (HttpStatusCode)((ObjectResult)actionResult).StatusCode;
            statusCode.Should().Be(HttpStatusCode.BadRequest);

            var actualResponse = (ErrorResponse)((ObjectResult)actionResult).Value;
            actualResponse.Errors.Count.Should().Be(1);
            actualResponse.Errors[0].title.Should().Be("Input Frequenty is entered");
        }

        [TestMethod]
        public void ReturnBadRequestWhenFrequencyLessThenZero()
        {
            req.Frequencty = -100;
            var actionResult = controller.InstallmentService(req);
            actionResult.Should().BeOfType<ObjectResult>();

            var statusCode = (HttpStatusCode)((ObjectResult)actionResult).StatusCode;
            statusCode.Should().Be(HttpStatusCode.BadRequest);

            var actualResponse = (ErrorResponse)((ObjectResult)actionResult).Value;
            actualResponse.Errors.Count.Should().Be(1);
            actualResponse.Errors[0].title.Should().Be("Input Frequenty is entered");
        }

        [TestMethod]
        public void ReturnSuccess()
        {
            var actionResult = controller.InstallmentService(req);
           
            var statusCode = (HttpStatusCode)((ObjectResult)actionResult).StatusCode;
            statusCode.Should().Be(HttpStatusCode.OK);

           
        }





    }
}