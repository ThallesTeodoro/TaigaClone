using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Taiga.Api.Utilities
{
    public class Validations
    {
        public static IDictionary<string, string> FormatViewModelErrors(ModelStateDictionary modelState)
        {
            IDictionary<string, string> errorList = new Dictionary<string, string>();

            foreach (var error in modelState)
            {
                if (error.Value.Errors.Any())
                {
                    errorList.Add(error.Key, error.Value.Errors.First().ErrorMessage);
                }
            }

            return errorList;
        }
    }
}
