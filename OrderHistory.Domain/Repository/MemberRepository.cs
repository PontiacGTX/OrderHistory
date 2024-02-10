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
    public class MemberRepository: IMemberRepository
    {
        private readonly DBContext _ctx;
        public MemberRepository(DBContext ctx)
        {
            _ctx = ctx;
        }
        public async Task<long> Count() => await _ctx.Member.LongCountAsync();
        public async Task<IList<Member>> Get() => await _ctx.Member.ToListAsync();
        public async Task<Member?> Get(long id)=> await _ctx.Member.FirstOrDefaultAsync(x=>x.Id == id);
        public async Task<Member?> Find(object id) => await _ctx.Member.FindAsync(id);
        public async Task<Member> Add(Member m)
        {
            if(m == null) throw new Exception("Null member value");

            var r = _ctx.Member.Add(m);
            await _ctx.SaveChangesAsync();
            return r.Entity;
        }
        public async Task<Member> Update(Member m)
        {
            var e = await Get(m.Id); if (e != null) throw new Exception($"Member with id {m.Id} not found");

            var eResult =_ctx.Entry(e)!;
            eResult.CurrentValues.SetValues(m);
            await _ctx.SaveChangesAsync();
            return eResult.Entity!;
        }
        public async Task<bool> Delete(long id)
        {
            var m = await Get(id);
            if (m == null) return true;
            _ctx.Member.Remove(m);
            var r =await _ctx.SaveChangesAsync();
            return r>0 || Get(id)==null;
        }


    }
}
