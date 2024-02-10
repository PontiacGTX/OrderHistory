using MediatR;
using OrderHistory.Data.Entity;
using OrderHistory.Data.Model;
using OrderHistory.Data.Query;
using OrderHistory.Domain.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderHistory.Domain.QueryHandler
{
    public class GetLastMemberOrderQueryHandler : IRequestHandler<GetLastMemberOrderQuery, Order>
    {
        private IOrderRepository _orderRepository;

        public GetLastMemberOrderQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<Order> Handle(GetLastMemberOrderQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _orderRepository.Last(request.MemberId);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
