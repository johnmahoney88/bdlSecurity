using Microsoft.AspNet.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bdlSecurity.Common
{
    public class ExceptionHelper
    {
        public static string ExceptionMessageToString(Exception ex)
        {
            StringBuilder sb = new StringBuilder(1024);
            Exception exCurr = ex;
            int i = 1;
            if (exCurr.InnerException == null)    // only one exception
            {
                sb.AppendFormat("{0}{1}", exCurr.Message, System.Environment.NewLine);
            }
            else
            {
                sb.Append(System.Environment.NewLine);
                while (exCurr != null)
                {
                    sb.Insert(0, string.Format("({0})   {1}{2}", i, exCurr.Message, System.Environment.NewLine));
                    exCurr = exCurr.InnerException;
                    i++;
                }
            }
            return sb.ToString();
        }
    }

    public class ValidationHelper
    {
        public static string ValidationMessageToString(ModelStateDictionary state, string separator=null)
        {

            state.Where(s => s.Value.ValidationState == ModelValidationState.Invalid);

            return string.Join(separator ?? System.Environment.NewLine,
                                state.Values.Where(E => E.Errors.Count > 0)
                                .SelectMany(E => E.Errors)
                                .Select(E => E.ErrorMessage)
                                .ToArray());
        }
    }
}
