using MediatR;
using OrderHistory.Data.Entity;
using OrderHistory.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderHistory.Data.Query
{
    public class GetLastMemberOrderQuery:IRequest<Order>
    {
        public long MemberId { get; set; }
    }
}
