using Nate.Core.Constants;

namespace Nate.Core.Exceptions
{
    public class BusinessException : ExceptionBase
    {
        public BusinessException(string message)
            : base(message, ApiResultCode.Error) { }

        public BusinessException(string message, int code)
            : base(message, code) { }
    }
}
