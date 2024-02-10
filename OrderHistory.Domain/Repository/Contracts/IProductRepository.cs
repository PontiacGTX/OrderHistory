using OrderHistory.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderHistory.Domain.Repository.Contracts
{
    public interface IProductRepository
    {
         Task<long> Count();
         Task<IList<Product>> Get();
         Task<Product?> Get(long id);
         Task<Product?> Find(object id);
         Task<Product> Add(Product m);
         Task<Product> Update(Product m);
         Task<bool> Delete(long id);
    }
}
