using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;

namespace Application.Users.Queries.GetUserDetail
{
    public class WalletLookupDto : IMapFrom<Wallet>
    {
        public Guid Identifier { get; private set; }
        public decimal Balance { get; set; }

        public IList<WallatTransactionLookupDto> Transactions { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Wallet, WalletLookupDto>()
                .ForMember(d => d.Balance, opt => opt.MapFrom(s => s.GetBalance()));
        }
    }
}