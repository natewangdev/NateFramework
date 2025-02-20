using Nate.Core.Constants;

namespace Nate.Core.Exceptions
{
    public abstract class ExceptionBase : Exception
    {
        public int Code { get; }

        protected ExceptionBase(string message, int code = ApiResultCode.Error) : base(message)
        {
            Code = code;
        }
    }
}
