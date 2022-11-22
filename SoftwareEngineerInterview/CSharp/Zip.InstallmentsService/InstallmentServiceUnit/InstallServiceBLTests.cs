using FluentAssertions;
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
        InstallServiceRequest req;
        Mock<ILogger> _log;
        PaymentPlan paymentPlan;


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
            paymentPlanFacBL = new PaymentPlanFactory(_log.Object);

            paymentPlan = new PaymentPlan();
            Installment installmentDet = new Installment();

            installmentDet.Id = Guid.NewGuid();
            installmentDet.DueDate = System.DateTime.Now;
            installmentDet.Amount = 1000;            Installment[] installments = { installmentDet };

            paymentPlan.Installments = installments;
            paymentPlan.Id  = Guid.NewGuid();
            paymentPlan.PurchaseAmount = 4;           

        }


        [TestCleanup]
        public void cleanup()
        {

        }


        [TestMethod]
        public void ReturnOKRequestWhenRequestIsProper()
        {
            var actionResult = paymentPlanFacBL.CreatePaymentPlan(req);
            actionResult.Should().BeOfType<PaymentPlan>();
            actionResult.Should().NotBeNull();

        }




    }
}
