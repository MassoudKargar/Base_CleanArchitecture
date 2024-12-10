using System.Net;

namespace Base.Sample.Application.RequestResponse.Responses
{
    public class BaseCommandResult
    {
        public bool Handled { get; set; }
        public BaseCommandResult()
        {
            this.Handled = true;
            this.Description = "Success";
        }
        public BaseCommandResult(bool handled, string desc)
        {
            this.Description = desc;
            this.Handled = handled;
        }
        public string Description { get; set; }
    }
    public class BaseCommandResult<T> : BaseCommandResult where T : class
    {
        public BaseCommandResult()
        {
            this.Description = "Success";
        }
        public BaseCommandResult(bool handled, string desc) : base(handled, desc)
        {
        }
    }
}
