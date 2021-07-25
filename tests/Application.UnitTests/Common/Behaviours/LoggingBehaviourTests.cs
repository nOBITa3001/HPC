using HPC.Application.Commands.Ships;
using HPC.Application.Common.Behaviours;
using HPC.Application.Common.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;

namespace HPC.Application.UnitTests.Common.Behaviours
{
    public class LoggingBehaviourTests
    {
        private readonly Mock<ILogger<CreateShipCommand>> _logger;
        private readonly Mock<ICurrentUserService> _currentUserService;
        private readonly Mock<IIdentityService> _identityService;

        public LoggingBehaviourTests()
        {
            _logger = new Mock<ILogger<CreateShipCommand>>();
            _currentUserService = new Mock<ICurrentUserService>();
            _identityService = new Mock<IIdentityService>();
        }

        [Test]
        public async Task ShouldCallGetUserNameAsyncOnceIfAuthenticated()
        {
            _currentUserService.Setup(service => service.UserId).Returns("Administrator");
            var behaviour = new LoggingBehaviour<CreateShipCommand>(_logger.Object, _currentUserService.Object, _identityService.Object);

            await behaviour.Process(Tools.Builders.CreateShipCommandBuilder.WithValidCreateShipCommand().Build(), new CancellationToken());

            _identityService.Verify(service => service.GetUserNameAsync(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public async Task ShouldNotCallGetUserNameAsyncOnceIfUnauthenticated()
        {
            var behaviour = new LoggingBehaviour<CreateShipCommand>(_logger.Object, _currentUserService.Object, _identityService.Object);

            await behaviour.Process(Tools.Builders.CreateShipCommandBuilder.WithValidCreateShipCommand().Build(), new CancellationToken());

            _identityService.Verify(service => service.GetUserNameAsync(null), Times.Never);
        }
    }
}
