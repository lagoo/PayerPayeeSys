using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;

namespace Application.Users.Queries.GetUserDetail
{
    public class WallatTransactionLookupDto : IMapFrom<WallatTransaction>
    {
        public decimal Amount { get; set; }
        public string OperationType { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<WallatTransaction, WallatTransactionLookupDto>()
                .ForMember(d => d.OperationType, opt => opt.MapFrom(s => s.OperationType.ToString()));
        }
    }
}