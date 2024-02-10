using Common.Response;
using Microsoft.AspNetCore.Mvc;
using OrderHistory.Data.Model;
using OrderHistory.Infraestructure.Services.Contracts;
using System.Net;

namespace OrderHistory.API.Controllers
{
    public class PostController : Controller
    {
        private ICommentsServices _commentServices;

        public PostController(ICommentsServices commentServices)
        {
            _commentServices = commentServices;
        }

        [HttpGet("Top")]
        public async Task<IActionResult> GetTopCommentInfo([FromQuery]int count=3)
        {
            try
            {
                if(count<1)
                {
                    return BadRequest(new
                    {
                        Data =new List<object>(),
                        Status=HttpStatusCode.BadRequest,
                        Message="Count cannot be lower than 1"
                    });
                }

                var result = await _commentServices.GetTopComments(count);
                if(result.Data ==null|| result.Data.Count==0)
                    return NotFound();
                if(result.Errors?.Keys?.Count is not 0 and not null)
                {
                    return StatusCode(
                        result.StatusCode, new
                        {
                            Data = result,
                            Status = result.StatusCode,
                            Message = result.Message
                        });
                }
                var response = new Response<IList<CommentMetadata>> 
                { 
                     Data = result.Data,Message = result.Message,StatusCode = result.StatusCode,Success =true
                };
                return Ok(new
                {
                    Data = response,
                    Status = HttpStatusCode.OK,
                    Message = "Ok"
                });
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
