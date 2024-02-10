using MediatR;
using OrderHistory.Data.Model;
using OrderHistory.Data.Query;
using OrderHistory.Infraestructure.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderHistory.Infraestructure.Services
{
    public class OrderServices: IOrderServices
    {
        private readonly IMediator _mediator;

        public OrderServices(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<LastMemberOrderModel> GetLastMemberOrder(GetLastMemberOrderQuery request)
        {
            var order = await _mediator.Send(request);
            if (order is null) return null;
            var member = await _mediator.Send(new GetMemberByIdQuery {  Id = order.MemberId });
            if (member is null) throw new Exception($"No member was found with id {order.Id}");
            var product = await _mediator.Send(new GetProductByIdQuery {  Id  =order.ProductId});
            var result = new LastMemberOrderModel { 
            
                OrderId = order.Id,
                MemberName = member.Name,
                ProductName =product?.Name!
            };
            return result;
        }

        public async Task<IList<LastMemberOrderModel>> GetLastMemberOrder()
        {
            return await _mediator.Send(new GetLastMembersOrderQuery());
        }
    }
}
