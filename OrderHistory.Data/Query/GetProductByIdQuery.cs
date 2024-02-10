using MediatR;
using OrderHistory.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderHistory.Data.Query
{
    public class GetProductByIdQuery:IRequest<Product>
    {
        public long Id { get; set; }
    }
}
