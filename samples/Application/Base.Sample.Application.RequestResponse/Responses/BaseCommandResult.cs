using System.Net;

namespace Base.Sample.Application.RequestResponse.Responses
{
    public class BaseCommandResult
    {
        public BaseCommandResult()
        {
            this.Description = "Success";
        }
        public BaseCommandResult(string desc)
        {
            this.Description = desc;
        }
        public string Description { get; set; }
    }
    public class BaseCommandResult<T> : BaseCommandResult where T : class
    {
        public BaseCommandResult()
        {
            this.Description = "Success";
        }
        public BaseCommandResult(string desc) : base(desc)
        {
        }
    }
}
