using Zip.InstallmentsService.Model;

namespace Zip.InstallmentServices.Validation
{


    public enum ValidationError
    {
        RequestEmptyError = 1,
        InvalidAmountError = 2,
        InvalidFreqeuncyError = 3,
        InvalidNoOfInstallmentError = 4,
       
    }

    public class Helper
    {
        public static void BuildContent(List<ErrorDetails> errors, ValidationError validationError)
        {
            var error = new ErrorDetails
            {
                code = (int)validationError,
                title = GetErrorMessage(validationError)
            };

            errors.Add(error);
        }

        public static string GetErrorMessage(ValidationError validationError)
        {
            switch (validationError)
            {
                case ValidationError.RequestEmptyError: return "Request is null";
                case ValidationError.InvalidAmountError: return "Invalid Amount is entered";
                case ValidationError.InvalidFreqeuncyError: return "Input Frequenty is entered";
                case ValidationError.InvalidNoOfInstallmentError: return "Invalid Number of Installments";
                default: return string.Empty;
            }
        }
    }
}
