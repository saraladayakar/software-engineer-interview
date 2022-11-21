using Newtonsoft.Json;
using System.Collections.Generic;


namespace Zip.InstallmentsService.Model
{
    public class ErrorResponse
    {
        public List<ErrorDetails> Errors { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.None,
                new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }
    }


    public class ErrorDetails
    {
        public int code { get; set; }
        public string title { get; set; }
    }
}
