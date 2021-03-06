using Application.UnitTests.Core;
using Application.Users.Queries.GetUserDetail;
using AutoMapper;
using FluentAssertions;
using Persistence;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.UnitTests.Users.Queries.GetUserDetail
{
    [Collection("QueryCollection")]
    public class GetUserDetailQueryTests
    {
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;

        public GetUserDetailQueryTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task Handle_GivenValidRequest_ShouldReturnUsersList()
        {
            var sut = new GetUserDetailQuery.Handler(_context, _mapper);

            var result = await sut.Handle(new GetUserDetailQuery(1), CancellationToken.None);

            result.Should().BeOfType<UserDetailVm>();

            result.Name.Should().Be(ApplicationContextFactory.UserWithBalance.Name);
            result.Document.Should().Be(ApplicationContextFactory.UserWithBalance.Document);
            result.Email.Should().Be(ApplicationContextFactory.UserWithBalance.Email);
            result.Wallet.Balance.Should().Be(100);
            result.Wallet.Transactions.Should().HaveCount(1);
        }
    }
}
