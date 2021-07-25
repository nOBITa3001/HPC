using FluentAssertions;
using HPC.Application.Commands.Ships;
using HPC.Application.Common.Exceptions;
using HPC.Domain.Entities;
using HPC.Domain.Enums;
using NUnit.Framework;
using System.Threading.Tasks;
using Tools.Builders;

namespace HPC.Application.IntegrationTests.Commands.Ships
{
    using static Testing;

    public class DeleteShipTests : TestBase
    {
        [Test]
        public void ShouldThrowValidationExceptionIfCommandIsInvalid()
        {
            var command = new DeleteShipCommand { Id = 0 };

            FluentActions.Invoking(() => SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Test]
        public void ShouldThrowNotFoundExceptionIfIdDoesNotExist()
        {
            var command = new DeleteShipCommand { Id = int.MaxValue };

            FluentActions.Invoking(() => SendAsync(command)).Should().Throw<NotFoundException>();
        }

        [Test]
        public async Task ShouldDeleteShip()
        {
            await RunAsDefaultUserAsync();
            var ship = (await SendAsync(CreateShipCommandBuilder.WithValidCreateShipCommand().Build())).Payload;

            await SendAsync(new DeleteShipCommand { Id = ship.Id });

            var actual = await FindAsync<Ship>(ship.Id);
            actual.RecordStatus.Should().Be(RecordStatus.Deleted);
        }
    }
}
