using Microsoft.AspNetCore.Mvc;
using Zip.InstallmentsService;

namespace InstallmentServices.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InstallmentServiceController : ControllerBase
    {
    //    private static readonly string[] Summaries = new[]
    //    {
    //    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    //};

    //    private readonly ILogger<WeatherForecastController> _logger;

    //    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    //    {
    //        _logger = logger;
    //    }

        [HttpPost]
        [Route("[InstallmentServiceDetails]")]
        public IActionResult InstallmentService(InstallServiceRequest request)
        {          
            //Validating the reqest
            List<string> str = ValidateRequest(request);
            if (str.Count > 0)
            {
                return BadRequest("Provided input is not in the correct format" + str);
            }
            PaymentPlanFactory obj = new PaymentPlanFactory();
            PaymentPlan responseObj = new PaymentPlan();

            responseObj =obj.CreatePaymentPlan(request);
          
            return Ok(responseObj);
        }


        private List<string> ValidateRequest(InstallServiceRequest request)
        {
            List<string> validateErr = new List<string>();

            if (request == null)
            {
                validateErr.Add("Provided reques is Empty");
            }

            else
            {
                if (request.Amount <= 0)
                {
                    validateErr.Add("Provided Amount is wrong");
                }

                if (request.Frequencty <= 0)
                {
                    validateErr.Add("Wrong Frequencty is entered");
                }

                if (request.NoOfInstallment <= 0)
                {
                    validateErr.Add("Wrong NoOfInstallment is entered");
                }
            }


            return validateErr;
        }
    }
}   