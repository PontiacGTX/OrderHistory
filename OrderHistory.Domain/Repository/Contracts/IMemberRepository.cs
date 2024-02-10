using OrderHistory.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderHistory.Domain.Repository.Contracts
{
    public interface IMemberRepository
    {
         Task<long> Count();
         Task<IList<Member>> Get();
         Task<Member?> Get(long id);
         Task<Member?> Find(object id);
         Task<Member> Add(Member m);
         Task<Member> Update(Member m);
         Task<bool> Delete(long id);
    }
}
