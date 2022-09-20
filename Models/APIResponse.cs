using ForestInteractiveTestApp.Common;

namespace ForestInteractiveTestApp.Models
{
    public class APIResponse
    {
        public int StatusCode { get; set; } = APIResponseCodes.StatusCodeOK;
        public string StatusMessage { get; set; } = Constant.SuccessResponse;
        public object Response { get; set; }
    }

    public class APIResponseCodes
    {

        public static int StatusCodeOK { get { return 0; } }
        public static int StatusCodeERROR { get { return 1; } }
    }
}
