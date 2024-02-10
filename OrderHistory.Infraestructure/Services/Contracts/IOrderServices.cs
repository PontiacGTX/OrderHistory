using OrderHistory.Data.Model;
using OrderHistory.Data.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderHistory.Infraestructure.Services.Contracts
{
    public interface IOrderServices
    {
        Task<LastMemberOrderModel> GetLastMemberOrder(GetLastMemberOrderQuery request);
        Task<IList<LastMemberOrderModel>> GetLastMemberOrder();
    }
}
