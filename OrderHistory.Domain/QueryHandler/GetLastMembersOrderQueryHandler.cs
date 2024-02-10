using MediatR;
using OrderHistory.Data.Model;
using OrderHistory.Data.Query;
using OrderHistory.Domain.Repository;
using OrderHistory.Domain.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderHistory.Domain.QueryHandler
{
    public class GetLastMembersOrderQueryHandler : IRequestHandler<GetLastMembersOrderQuery, IList<LastMemberOrderModel>>
    {
        private IOrderRepository _orderRepository;

        public GetLastMembersOrderQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<IList<LastMemberOrderModel>> Handle(GetLastMembersOrderQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _orderRepository.GetLastMembersOrder();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
