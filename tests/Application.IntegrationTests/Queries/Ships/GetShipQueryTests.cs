using FluentAssertions;
using HPC.Application.Common.Exceptions;
using HPC.Application.Dtos.Ships;
using HPC.Application.TodoLists.Queries.GetTodos;
using NUnit.Framework;
using System.Threading.Tasks;
using Tools.Builders;

namespace HPC.Application.IntegrationTests.Queries.Ships
{
    using static Testing;

    public class GetShipQueryTests : TestBase
    {
        [Test]
        public void ShouldThrowValidationExceptionIfCommandIsInvalid()
        {
            var command = new GetShipQuery { Id = 0 };

            FluentActions.Invoking(() => SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Test]
        public void ShouldThrowNotFoundExceptionIfIdDoesNotExist()
        {
            var command = new GetShipQuery { Id = int.MaxValue };

            FluentActions.Invoking(() => SendAsync(command)).Should().Throw<NotFoundException>();
        }

        [Test]
        public async Task ShouldReturnShip()
        {
            await RunAsDefaultUserAsync();
            var ship = (await SendAsync(CreateShipCommandBuilder.WithValidCreateShipCommand().Build())).Payload;
            var query = new GetShipQuery() { Id = ship.Id };

            var actual = (await SendAsync(query));

            var payload = actual.Payload;
            ShouldBeSameShip(payload, ship);
        }

        private static void ShouldBeSameShip(ShipDto actual, ShipDto expected)
        {
            actual.Id.Should().Be(expected.Id);
            actual.Code.Should().Be(expected.Code);
            actual.LengthInMetres.Should().Be(expected.LengthInMetres);
            actual.WidthInMetres.Should().Be(expected.WidthInMetres);
            actual.CreatedInUtc.Should().BeCloseTo(expected.CreatedInUtc, 10000);
            actual.ModifiedInUtc.Should().BeCloseTo(expected.ModifiedInUtc, 10000);
        }
    }
}
