using Microsoft.EntityFrameworkCore;
using OrderHistory.Data.Entity;
using OrderHistory.Domain.Context;
using OrderHistory.Domain.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderHistory.Domain.Repository
{
    public class ProductRepository: IProductRepository
    {
        private DBContext _ctx;

        public ProductRepository(DBContext ctx)
        {
            _ctx = ctx;
        }
        public async Task<long> Count() => await _ctx.Product.LongCountAsync();
        public async Task<IList<Product>> Get() => await _ctx.Product.ToListAsync();
        public async Task<Product?> Get(long id) => await _ctx.Product.FirstOrDefaultAsync(x => x.Id == id);
        public async Task<Product?> Find(object id) => await _ctx.Product.FindAsync(id);
        public async Task<Product> Add(Product m)
        {
            if (m == null) throw new Exception("Null Product value");

            var r = _ctx.Product.Add(m);
            await _ctx.SaveChangesAsync();
            return r.Entity;
        }
        public async Task<Product> Update(Product m)
        {
            var e = await Get(m.Id); if (e != null) throw new Exception($"Product with id {m.Id} not found");

            var eResult = _ctx.Entry(e)!;
            eResult.CurrentValues.SetValues(m);
            await _ctx.SaveChangesAsync();
            return eResult.Entity!;
        }
        public async Task<bool> Delete(long id)
        {
            var m = await Get(id);
            if (m == null) return true;
            _ctx.Product.Remove(m);
            var r = await _ctx.SaveChangesAsync();
            return r > 0 || Get(id) == null;
        }
    }
}
