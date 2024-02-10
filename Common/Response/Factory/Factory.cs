using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Response.Factory
{
    public static class Factory
    {
        public static Response<T> Ok<T>(T data)
        {
            return new Response<T>
            {
                Data = data,
                Message = "Ok",
                StatusCode = 200,
                Success = true
            };
        }
    }
}
