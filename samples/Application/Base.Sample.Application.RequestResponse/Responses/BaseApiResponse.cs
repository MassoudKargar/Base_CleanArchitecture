using System.Net;

namespace Base.Sample.Application.RequestResponse.Responses
{
    public class BaseApiResponse
    {
        public BaseApiResponse(HttpStatusCode statusCode)
        {
            this.StatusCode = statusCode;
        }
        public HttpStatusCode StatusCode { get; set; }
        public string Description { get; set; }
    }
    public class BaseApiResponse<T> : BaseApiResponse where T : class
    {
        public BaseApiResponse(HttpStatusCode statusCode) : base(statusCode)
        {
        }
    }
}
