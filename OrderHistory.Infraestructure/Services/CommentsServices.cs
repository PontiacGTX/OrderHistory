using Common.ServicesModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Response;
using OrderHistory.Data;
using OrderHistory.Data.Model;
using System.Net.Http;
using System.Text.Json;
using OrderHistory.Infraestructure.Services.Contracts;

namespace OrderHistory.Infraestructure.Services;

public class CommentsServices: ICommentsServices
{

    private readonly HttpClient _client;
    private readonly Uri? _url;

    public CommentsServices(IHttpClientFactory httpClientFactory )
    {
        _client = httpClientFactory.CreateClient("CommentsClient");
        _url = _client.BaseAddress;
    }

    /// <summary>
    /// returns a response with a list of the top 3 comments
    /// </summary>
    /// <see cref="Response{IList{CommentResponse}}"/>
    /// <see cref="ValidationServerResponse{IList{CommentResponse}}<>"/>
    /// <returns></returns>
    public async Task<ValidationServerResponse<IList<CommentMetadata>>> GetTopComments(int count=3)
    {
        ValidationServerResponse<IList<CommentMetadata>> response = new ValidationServerResponse<IList<CommentMetadata>>();
        HttpResponseMessage responseMessage = null;
        try
        {
             responseMessage =await _client.GetAsync(_url);
            if (!responseMessage.IsSuccessStatusCode)
            {
                response.StatusCode = (int)responseMessage.StatusCode;
                response.Message = "Unexpected Response for comment service service";
                return response;

            }
            var str =await responseMessage.Content.ReadAsStringAsync();
            var data= JsonSerializer.Deserialize<IList<CommentResponse>>(str);
            response.Data = data.GroupBy(x=>x.PostId).OrderBy(x=>x.Count()).Select(x => new CommentMetadata
            {
                CommentCount = x.Count(),
                PostId =x.Key
            }).Take(count> data.Count? data.Count-1: count).ToList();
            response.StatusCode = data.Any() ?  200:404;
            response.Message = data.Any() ? "Ok" : "Not Found";
            return response;
        }
        catch (Exception ex)
        {
            throw new Exception(responseMessage?.ReasonPhrase, ex);
        }

    }

}
