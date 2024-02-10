
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Extensions
{
    public static class ModelStateHelper
    {
        public static List<KeyValuePair<string,List<string>>> GetErrors(this ModelStateDictionary ModelState)
        {
            return ModelState.Keys.SelectMany(key => ModelState[key].Errors.Select(val => new KeyValuePair<string, string>(key, val.ErrorMessage))).GroupBy(x => x.Key).Select(x => new KeyValuePair<string, List<string>>(x.FirstOrDefault().Key, x.Select(x => x.Value).ToList())).ToList();
        }
    }
}
