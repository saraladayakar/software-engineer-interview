using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zip.InstallmentsService.Model
{
    public class InstallServiceRequest
    {
        public DateTime DateofOrder { get; set; }
        public decimal Amount { get; set; }
        public int NoOfInstallment { get; set; }
        public int Frequencty { get; set; }
    }
}
