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
    public class GetMemberByIdQueryHandler : IRequestHandler<GetMemberByIdQuery, Member>
    {
        private IMemberRepository _memberRepository;

        public GetMemberByIdQueryHandler(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }
        public async Task<Member> Handle(GetMemberByIdQuery request, CancellationToken cancellationToken)
        {
			try
			{
                return await _memberRepository.Get(request.Id)!;

            }
			catch (Exception ex)
			{

				throw;
			}
        }
    }
}
