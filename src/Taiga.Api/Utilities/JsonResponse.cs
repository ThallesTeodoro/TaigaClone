using System.Collections.Generic;

namespace Taiga.Api.Utilities
{
    public class JsonResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        public IDictionary<string, string> Errors { get; set; }

        public JsonResponse(int StatusCode = 200, string Message = "Ok")
        {
            this.StatusCode = StatusCode;
            this.Message = Message;
        }

        public JsonResponse(int StatusCode = 200)
        {
            this.StatusCode = StatusCode;
        }

        public JsonResponse()
        {
            ///
        }
    }
}
