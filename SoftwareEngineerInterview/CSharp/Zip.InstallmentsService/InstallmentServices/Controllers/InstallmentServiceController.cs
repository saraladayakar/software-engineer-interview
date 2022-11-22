using Microsoft.AspNetCore.Mvc;
using System.Net;
using Zip.InstallmentServices.Validation;
using Zip.InstallmentsService;
using Zip.InstallmentsService.Model;

namespace InstallmentServices.Controllers
{

    [Route("Api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
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

        private ILogger log;
        private readonly IPlaymentPlanFactory updateBL;

        public InstallmentServiceController(IPlaymentPlanFactory bl, ILogger _log)
        {
            updateBL = bl;
            log = _log;
            
        }




        [HttpPost]
        [Route("[InstallmentServiceDetails]")]
         public IActionResult InstallmentService([FromBody] InstallServiceRequest request)
        {          
            //Validating the reqest
           var str = ValidateRequest(request);

            if (str.Count > 0)
            {
                var errorResponse = new ErrorResponse();
                errorResponse.Errors = str;
                return StatusCode((int)HttpStatusCode.BadRequest, errorResponse);
            }                      
         
            var responseObj = updateBL.CreatePaymentPlan(request);

            if (responseObj == null)
            {
                return StatusCode((int)HttpStatusCode.UnprocessableEntity, "Exception on Calculating Installment");

            }

            return Ok(responseObj);
        }


        private List<ErrorDetails> ValidateRequest(InstallServiceRequest request)
        {
            var validateErr = new List<ErrorDetails>();

            if (null == request)
                Helper.BuildContent(validateErr, ValidationError.RequestEmptyError);

            else
            {
                if (request.Amount <= 0)
                {
                    Helper.BuildContent(validateErr, ValidationError.InvalidAmountError);
                }

                if (request.Frequencty <= 0)
                {
                    Helper.BuildContent(validateErr, ValidationError.InvalidFreqeuncyError);
                }

                if (request.NoOfInstallment <= 0)
                {
                    Helper.BuildContent(validateErr, ValidationError.InvalidNoOfInstallmentError);
                }
            }


            return validateErr;
        }
    }
}   