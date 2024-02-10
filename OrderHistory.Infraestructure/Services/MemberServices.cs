using MediatR;
using OrderHistory.Infraestructure.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderHistory.Infraestructure.Services
{
    public class MemberServices:IMemberServices
    {
        private Mediator _mediator;

        public MemberServices(Mediator mediator)
        {
            _mediator = mediator;
        }
        
    }
}
