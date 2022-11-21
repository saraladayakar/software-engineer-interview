using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using InstallmentServices.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Zip.InstallmentsService;
using Zip.InstallmentsService.Model;

namespace InstallmentServiceUnit
{
    [TestClass]
    public  class InstallServiceBLTests
    {

        IPlaymentPlanFactory paymentPlanFacBL;
        //Mock<IPlaymentPlanFactory> mockFactory;
        InstallServiceRequest req;
        Mock<ILogger> _log;
        PaymentPlan paymentPlan;


        [TestInitialize]
        public void Setup()
        {
           // controllerObj = new InstallmentServiceController();
            req = new InstallServiceRequest
            {
                NoOfInstallment = 4,
                Amount = 1000,
                DateofOrder = System.DateTime.Now,
                Frequencty = 15
            };


            _log = new Mock<ILogger>();
            paymentPlanFacBL = new PaymentPlanFactory(_log.Object);

            paymentPlan = new PaymentPlan();

            Installment installObj = new Installment();
            installObj.Id = Guid.NewGuid();
            installObj.DueDate = System.DateTime.Now;
            installObj.Amount = 1000;

            paymentPlan.Id  = Guid.NewGuid();
            paymentPlan.PurchaseAmount = 4;
            paymentPlan.Installments[0] = installObj;

        }


        [TestCleanup]
        public void cleanup()
        {

        }


        [TestMethod]
        public void ReturnBadRequestWhenRequestNull()
        {
            var response = paymentPlanFacBL.CreatePaymentPlan(req);
            //ObjectResult objectResponse = Assert.IsType<ObjectResult>(response);
            //Assert.Equal(200, objectResponse.StatusCode);
            response.Should().Be(StatusCodes.Status200OK);
            response.Should().Be(paymentPlan);
            
        }

    }
}
