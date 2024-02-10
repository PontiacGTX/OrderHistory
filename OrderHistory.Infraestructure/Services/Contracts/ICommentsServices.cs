using Common.Response;
using OrderHistory.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderHistory.Infraestructure.Services.Contracts
{
    public interface ICommentsServices
    {
        Task<ValidationServerResponse<IList<CommentMetadata>>> GetTopComments(int count = 3);
    }
}
