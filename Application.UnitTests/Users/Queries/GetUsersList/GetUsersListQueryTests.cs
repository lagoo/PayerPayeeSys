using Application.UnitTests.Core;
using Application.Users.Queries.GetUsersList;
using AutoMapper;
using FluentAssertions;
using Persistence;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.UnitTests.Users.Queries.GetUsersList
{
    [Collection("QueryCollection")]
    public class GetUsersListQueryTests
    {
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;

        public GetUsersListQueryTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;            
        }

        [Fact]
        public async Task Handle_GivenValidRequest_ShouldReturnUsersList()
        {
            var sut = new GetUsersListQuery.Handler(_context, _mapper);

            var result = await sut.Handle(new GetUsersListQuery(), CancellationToken.None);

            result.Should().BeOfType<UserListVm>();

            result.Users.Should().HaveCount(ApplicationContextFactory.Users.Length);
            result.Users.First().Name.Should().Be(ApplicationContextFactory.UserWithBalance.Name);
        }
    }    
}
