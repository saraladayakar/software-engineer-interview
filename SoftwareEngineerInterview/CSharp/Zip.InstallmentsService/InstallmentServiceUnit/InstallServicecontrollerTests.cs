using FluentAssertions;
using InstallmentServices.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Zip.InstallmentsService;


namespace InstallmentServiceUnit
{
    [TestClass]
    public class InstallServicecontrollerTests
    {

        InstallmentServiceController obj;
        Mock<PaymentPlanFactory> mockFactory;
        InstallServiceRequest req;


       [TestInitialize]
        public void Setup()
        {
            obj = new InstallmentServiceController();
            req = new InstallServiceRequest
            {
                NoOfInstallment = 4,
                Amount = 1000,
                DateofOrder = System.DateTime.Now,
                Frequencty = 15
            };
          
            mockFactory = new Mock<PaymentPlanFactory>();
            mockFactory.Setup(x => x.CreatePaymentPlan(req)).Returns(It.IsAny<PaymentPlan>());
                

        }
        [TestCleanup]
        public void cleanup() {
        
        }


        [TestMethod]
        public void ReturnBadRequestWhenRequestNull()
        {
            req = null;
            IActionResult response = obj.InstallmentService(req);          
            response.Should().Be(StatusCodes.Status400BadRequest);
        }

        [TestMethod]
        public void ReturnBadRequestWhenAmountZero()
        {
            req.Amount =0;
            IActionResult response = obj.InstallmentService(req);
            response.Should().Be(StatusCodes.Status400BadRequest);
        }

        [TestMethod]
        public void ReturnBadRequestWhenAmountLessThenZero()
        {
            req.Amount = -100;
            IActionResult response = obj.InstallmentService(req);
            response.Should().Be(StatusCodes.Status400BadRequest);
        }



        [TestMethod]
        public void ReturnBadRequestWhenFrequencyyZero()
        {
            req.Frequencty =0;
            IActionResult response = obj.InstallmentService(req);
            response.Should().Be(StatusCodes.Status400BadRequest);
        }

        [TestMethod]
        public void ReturnBadRequestWhenFrequencyLessThenZero()
        {
            req.Frequencty = -100;
            IActionResult response = obj.InstallmentService(req);

            //ObjectResult objectResponse = Assert.IsType<ObjectResult>(response);

            ////Assert.AreEqual(400, response.StatusCode);
            //Assert.IsInstanceOfType(result, typeof(JsonResult));


            //Assert.Equal(200, objectResponse.StatusCode);


            response.Should().Be(StatusCodes.Status400BadRequest);
        }

        [TestMethod]
        public void ReturnSuccess()
        {
            IActionResult response = obj.InstallmentService(req);
            //ObjectResult objectResponse = Assert.IsType<ObjectResult>(response);
            //Assert.Equal(200, objectResponse.StatusCode);
            response.Should().Be(StatusCodes.Status200OK);
        }





    }
}