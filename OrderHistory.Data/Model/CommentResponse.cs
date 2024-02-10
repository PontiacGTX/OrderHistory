using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OrderHistory.Data.Model
{
    public class CommentResponse
    {
        [JsonPropertyName("postId")]
        public int PostId { get; set; }
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("email")]
        public string Email { get; set; }
        [JsonPropertyName("body")]
        public string Body { get; set; }
       
    }
}
