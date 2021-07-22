using Application.Common.Interfaces;
using Common.Constants;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UnitTests.Core
{
    public static class MockExtensions
    {        
        public static Mock<ICurrentUserService> SetupService(this Mock<ICurrentUserService> mock )
        {
            mock.Setup(e => e.UserId).Returns(SystemConst.SYSTEM_USER_ID);
            mock.Setup(e => e.UserName).Returns(SystemConst.SYSTEM_USER_NAME);

            return mock;
        }
    }
}
