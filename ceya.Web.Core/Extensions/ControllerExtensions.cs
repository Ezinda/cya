using ceya.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ceya.Web.Core.Extensions
{
    public static class ControllerExtensions
    {
        public static void AddModelErrors(this Controller controller, IEnumerable<ValidationResult> validationResults, string defaultErrorKey = null)
        {
            if (validationResults != null)
            {
                foreach (var validationResult in validationResults)
                {
                    if (!string.IsNullOrEmpty(validationResult.MemberName))
                    {
                        controller.ModelState.AddModelError(validationResult.MemberName, validationResult.Message);
                    }
                    else if (defaultErrorKey != null)
                    {
                        controller.ModelState.AddModelError(defaultErrorKey, validationResult.Message);
                    }
                    else
                    {
                        controller.ModelState.AddModelError(string.Empty, validationResult.Message);
                    }
                }
            }
        }

        public static void AddModelErrors(this ModelStateDictionary modelState, IEnumerable<ValidationResult> validationResults, string defaultErrorKey = null)
        {
            if (validationResults == null) return;

            foreach (var validationResult in validationResults)
            {
                string key = validationResult.MemberName ?? defaultErrorKey ?? string.Empty;
                modelState.AddModelError(key, validationResult.Message);
            }
        }

        public static JsonResult AjaxModelErrors(this Controller controller)
        {
            var errorModel =
                        from x in controller.ModelState.Keys
                        where controller.ModelState[x].Errors.Count > 0
                        select new
                        {
                            key = x,
                            errors = controller.ModelState[x].Errors.
                                                          Select(y => y.ErrorMessage).
                                                          ToArray()
                        };
            return new JsonResult()
            {
                Data = errorModel
            };
        }
    }
}
