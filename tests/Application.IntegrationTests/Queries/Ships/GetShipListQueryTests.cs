using FluentAssertions;
using HPC.Application.Dtos.Ships;
using HPC.Application.TodoLists.Queries.GetTodos;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;
using Tools.Builders;

namespace HPC.Application.IntegrationTests.Queries.Ships
{
    using static Testing;

    public class GetShipListQueryTests : TestBase
    {
        [Test]
        public async Task ShouldReturnAllLists()
        {
            var userId = await RunAsDefaultUserAsync();
            var ship1 = (await SendAsync(CreateShipCommandBuilder.WithValidCreateShipCommand().Build())).Payload;
            var ship2 = (await SendAsync(CreateShipCommandBuilder.WithValidCreateShipCommand().Build())).Payload;
            var query = new GetShipListQuery(page: 1, size: 30);

            var actual = (await SendAsync(query));

            var payload = actual.Payload;
            payload.Should().HaveCount(2);
            ShouldBeSameShip(payload.First(ship => ship.Code == ship1.Code), ship1);
            ShouldBeSameShip(payload.First(ship => ship.Code == ship2.Code), ship2);

            var meta = actual.Meta;
            meta.Page.Should().Be(1);
            meta.TotalPages.Should().Be(1);
            meta.TotalCount.Should().Be(2);
            meta.HasPreviousPage.Should().BeFalse();
            meta.HasNextPage.Should().BeFalse();
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
