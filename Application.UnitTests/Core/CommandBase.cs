using Application.Common.Interfaces;
using Moq.AutoMock;
using Persistence;
using System;

namespace Application.UnitTests.Core
{
    public class CommandBase : IDisposable
    {
        protected readonly ApplicationContext _context;
        protected readonly AutoMocker autoMocker;        

        public CommandBase()
        {
            _context = ApplicationContextFactory.Create();

            autoMocker = new AutoMocker();
            autoMocker.Use(typeof(IApplicationContext), _context);
        }

        public void Dispose()
        {
            ApplicationContextFactory.Destroy(_context);
        }
    }
    
}
