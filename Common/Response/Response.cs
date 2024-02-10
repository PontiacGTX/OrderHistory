using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Common.Response
{
    public class Response<T>
    {
        public T Data { get; set; }
        public int StatusCode { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
    public class ValidationServerResponse<T> : Response<T>

    {
        [JsonPropertyName("errors")]
        public Dictionary<string, List<string>> Errors { get; set; }
    }
}
