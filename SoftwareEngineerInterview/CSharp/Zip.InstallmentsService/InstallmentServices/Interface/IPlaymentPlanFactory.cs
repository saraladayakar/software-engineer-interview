using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zip.InstallmentsService.Model;

namespace Zip.InstallmentsService
{
    public interface IPlaymentPlanFactory
    {
        PaymentPlan CreatePaymentPlan(InstallServiceRequest request);
    }
}
