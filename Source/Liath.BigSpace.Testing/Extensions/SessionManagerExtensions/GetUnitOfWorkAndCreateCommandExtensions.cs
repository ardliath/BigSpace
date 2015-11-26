using Liath.BigSpace.Session;
using Moq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liath.BigSpace.Testing.Extensions.SessionManagerExtensions
{
    public static class GetUnitOfWorkAndCreateCommandExtensions
    {
        public static Mock<ISessionManager> SetupUoWCreateCommand(this Mock<ISessionManager> sessionManager, IDbCommand command)
        {
            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.Setup(x => x.CreateCommand(It.IsAny<string>())).Returns(command);
            sessionManager.Setup(x => x.GetCurrentUnitOfWork()).Returns(unitOfWork.Object);
            return sessionManager;
        }
    }
}
