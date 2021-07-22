using Application.Common.Interfaces;
using Moq;
using Persistence;
using System;

namespace Application.UnitTests.Core
{
    public class CommandTestBase : IDisposable
    {
        protected readonly ApplicationContext _context;
        protected readonly Mock<ICurrentUserService> _currentUserMock;

        public CommandTestBase()
        {
            _context = ApplicationContextFactory.Create();
            _currentUserMock = new Mock<ICurrentUserService>();
        }

        public void Dispose()
        {
            ApplicationContextFactory.Destroy(_context);
        }
    }
}
