using bdlSecurity.Common;
using Microsoft.AspNet.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace bdlSecurity.Common
{
    public class ResultError
    {
        public static ResultError CreateFromModelState(HttpStatusCode status, ModelStateDictionary state)
        {
            var result = new ResultError();
            result.StatusCode = status;
            result.ErrorMessage = ValidationHelper.ValidationMessageToString(state);
            return result;
        }
        public static ResultError CreateFromException(string contextMessage, HttpStatusCode status, Exception e)
        {
            var result = new ResultError();
            result.StatusCode = status;

            result.ErrorMessage = string.Format("{0} {1}", (contextMessage ?? string.Empty), ExceptionHelper.ExceptionMessageToString(e));
            return result;
        }

        public HttpStatusCode StatusCode { get; set; }
        public string ErrorMessage { get; set; }

        public override string ToString()
        {
            return string.Format("ErrorMessage={0};Code={1}", ErrorMessage,StatusCode);
        }
    }
}
