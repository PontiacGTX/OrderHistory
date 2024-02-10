using MediatR;
using OrderHistory.Data.Entity;
using OrderHistory.Data.Query;
using OrderHistory.Domain.Repository;
using OrderHistory.Domain.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderHistory.Domain.QueryHandler
{
    public class GetProductByIdQueryHandler:IRequestHandler<GetProductByIdQuery,Product>
    {
        private IProductRepository _productRepository;

        public GetProductByIdQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Product> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _productRepository.Get(request.Id)!;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
