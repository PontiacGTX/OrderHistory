using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderHistory.Infraestructure.Services.Contracts;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace OrderHistory.API.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderServices _ordServices;

        public OrderController(IOrderServices ordServices)
        {
            _ordServices = ordServices;
        }


        [HttpGet("Last")]
        public async Task<IActionResult> GetLastMemberOrderDetail()
        {
            try
            {

                var result = await _ordServices.GetLastMemberOrder();
                return Ok(new 
                { 
                     Data = result,
                     Status=HttpStatusCode.OK,
                     Message ="Ok"
                });

            }
            catch (Exception ex)
            {

                throw;
            }
        }
        



    }
}
