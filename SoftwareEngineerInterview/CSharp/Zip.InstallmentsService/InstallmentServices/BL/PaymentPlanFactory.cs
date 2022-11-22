using Zip.InstallmentsService.Model;

namespace Zip.InstallmentsService
{
    /// <summary>
    /// This class is responsible for building the PaymentPlan according to the Zip product definition.
    /// </summary>
    public class PaymentPlanFactory : IPlaymentPlanFactory
    {
        /// <summary>
        /// Builds the PaymentPlan instance.
        /// </summary>
        /// <param name="purchaseAmount">The total amount for the purchase that the customer is making.</param>
        /// <returns>The PaymentPlan created with all properties set.</returns>



        private ILogger log;
       

        public PaymentPlanFactory(ILogger _log)
        {
          
            log = _log;

        }


        public PaymentPlan CreatePaymentPlan(InstallServiceRequest request)
        {
            PaymentPlan responseObj = new PaymentPlan();
            try
            {
                responseObj.Id = Guid.NewGuid();
                responseObj.PurchaseAmount = request.Amount;
                Installment[] installment = new Installment[request.NoOfInstallment];

                var finalAmount = request.Amount / request.NoOfInstallment;

                DateTime due = request.DateofOrder;

                for (int j = 0; j < request.NoOfInstallment; j++)
                {
                    Installment installmentObj = new Installment();
                    installmentObj.Id = Guid.NewGuid();
                    installmentObj.Amount = finalAmount;
                    installmentObj.Amount = finalAmount;
                    if (j==0)
                        installmentObj.DueDate = due;
                    else
                    {
                        installmentObj.DueDate = due.AddDays(request.Frequencty);
                        due = installmentObj.DueDate;
                    }

                    installment[j]= installmentObj;
                }
                responseObj.Installments = installment;

                return responseObj;

            } catch (Exception ex)
            {
                responseObj = null;
                throw;
            }
           
        }
    }
}
