using Common.Extensions;
using Common.Response;
using Common.Response.Factory;
using Common.Validation.Order;
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
        [GetTopCommentInfoRequestValidation]
        public async Task<IActionResult> GetTopCommentInfo([FromQuery]int count=3)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(new
                    {
                        Data = new List<object>(),
                        Status = HttpStatusCode.BadRequest,
                        Message = "Bad request",
                        Errors = ModelState.GetErrors()
                    });
                }

                var result = await _commentServices.GetTopComments(count);
                if(result.Data ==null|| result.Data.Count==0)
                    return NotFound();
                if(result.Errors?.Keys?.Count is not 0 and not null)
                {
                    return StatusCode(
                        result.StatusCode,result);
                }
                
                return Ok(Factory.Ok(result.Data));
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
