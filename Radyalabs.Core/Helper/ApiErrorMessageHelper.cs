using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.ModelBinding;

namespace Radyalabs.Core.Helper
{
    public class ApiErrorMessageHelper
    {
        public static string Error(ModelStateDictionary param)
        {
            StringBuilder sb = new StringBuilder();
            if (!param.IsValid)
            {
                if (param.Count > 0)
                {
                    foreach (var error in param)
                    {
                        if (error.Value.Errors.Count > 0)
                        {
                            if (error.Value.Errors[0].ErrorMessage != "")
                            {
                                sb.Append(string.Format("{0} ", error.Value.Errors[0].ErrorMessage));
                            }
                            else
                            {
                                sb.Append(string.Format("{0} ", error.Value.Errors[0].Exception.Message));
                            }
                        }
                    }
                }

                return sb.ToString();
            }

            return null;
        }
    }
}
