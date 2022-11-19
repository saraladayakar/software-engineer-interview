using System;

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
        public PaymentPlan CreatePaymentPlan(InstallServiceRequest request)
        {
            // TODO

            PaymentPlan responseObj = new PaymentPlan();

            responseObj.Id = Guid.NewGuid();
            responseObj.PurchaseAmount = request.Amount;

            Installment[] installment= new Installment[request.Frequencty];
            
           
            var finalAmount = request.Amount / request.NoOfInstallment;

            for ( int j =1; j <= request.Frequencty;  j ++)
            {
                installment[j].Id = Guid.NewGuid();
                installment[j].Amount = finalAmount;
                installment[j].DueDate  = request.DateofOrder.AddDays(request.Frequencty);
            }
            responseObj.Installments = installment;

            return responseObj;
        }
    }
}
