using Microsoft.EntityFrameworkCore;
using OrderHistory.Data.Entity;
using OrderHistory.Data.Model;
using OrderHistory.Domain.Context;
using OrderHistory.Domain.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderHistory.Domain.Repository
{
    public class OrderRepository: IOrderRepository
    {
        private readonly DBContext _ctx;

        public OrderRepository(DBContext ctx)
        {
            _ctx = ctx; 
        }
        public async Task<long> Count() 
            => await _ctx.Order.LongCountAsync();
        public async Task<IList<Order>> Get()
            => await _ctx.Order.ToListAsync();
        public async Task<Order?> Get(long id) 
            => await _ctx.Order.FirstOrDefaultAsync(x => x.Id == id);
        public async Task<Order?> Find(object id) 
            => await _ctx.Order.FindAsync(id);
        public async Task<Order?> Last(long memberId) 
            => await _ctx.Order
                                .Where(x=>x.MemberId == memberId)
                                .OrderBy(x => x.DateOrder)
                                .LastOrDefaultAsync();
        public async Task<Order> Add(Order m)
        {
            if (m == null) throw new Exception("Null Order value");

            var r = _ctx.Order.Add(m);
            await _ctx.SaveChangesAsync();
            return r.Entity;
        }
        public async Task<Order> Update(Order m)
        {
            var e = await Get(m.Id); if (e != null) throw new Exception($"Order with id {m.Id} not found");

            var eResult = _ctx.Entry(e)!;
            eResult.CurrentValues.SetValues(m);
            await _ctx.SaveChangesAsync();
            return eResult.Entity!;
        }
        public async Task<bool> Delete(long id)
        {
            var m = await Get(id);
            if (m == null) return true;
            _ctx.Order.Remove(m);
            var r = await _ctx.SaveChangesAsync();
            return r > 0 || Get(id) == null;
        }
        public async Task<IList<LastMemberOrderModel>> GetLastMembersOrder()
        {
         
            return _ctx.Order
             .Include(x => x.Member)
             .Include(x => x.Product)
             .GroupBy(x => x.MemberId)
             .AsEnumerable()
             .Select(group => group.OrderByDescending(x => x.DateOrder).FirstOrDefault())
             .Select(x => new LastMemberOrderModel
             {
                 MemberName = x.Member.Name,
                 OrderId = x.Id,
                 ProductName = x.Product.Name

             }).ToList();
        }
        
    }
}
