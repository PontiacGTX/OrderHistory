using OrderHistory.Data.Entity;
using OrderHistory.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderHistory.Domain.Repository.Contracts
{
    public interface IOrderRepository
    {
        Task<long> Count();
        Task<IList<Order>> Get();
        Task<Order?> Get(long id);
        Task<Order?> Find(object id);
        Task<Order?> Last(long memberId);
        Task<Order> Add(Order m);
        Task<Order> Update(Order m);
        Task<bool> Delete(long id);
        Task<IList<LastMemberOrderModel>> GetLastMembersOrder();
    }
}
