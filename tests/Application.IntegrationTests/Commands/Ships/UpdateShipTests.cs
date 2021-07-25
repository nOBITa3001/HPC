using FluentAssertions;
using HPC.Application.Commands.Ships;
using HPC.Application.Common.Exceptions;
using HPC.Domain.Entities;
using HPC.Domain.Enums;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using Tools.Builders;
using static Testing;

namespace HPC.Application.IntegrationTests.Ships.Commands
{
    public class UpdateShipTests : TestBase
    {
        static readonly UpdateShipCommand[] _invalidCommands =
        {
            UpdateShipCommandBuilder
                .WithValidUpdateShipCommand()
                .With(builder => builder.Name = string.Empty)
                .Build(),
            UpdateShipCommandBuilder
                .WithValidUpdateShipCommand()
                .With(builder => builder.Name = new string('A', 201))
                .Build(),
            UpdateShipCommandBuilder
                .WithValidUpdateShipCommand()
                .With(builder => builder.Code = string.Empty)
                .Build(),
            UpdateShipCommandBuilder
                .WithValidUpdateShipCommand()
                .With(builder => builder.Code = "Invalid Code")
                .Build(),
            UpdateShipCommandBuilder
                .WithValidUpdateShipCommand()
                .With(builder => builder.LengthInMetres = 0)
                .Build(),
            UpdateShipCommandBuilder
                .WithValidUpdateShipCommand()
                .With(builder => builder.WidthInMetres = 0)
                .Build()
        };

        [Test]
        public void ShouldThrowValidationExceptionIfCommandIsInvalid()
        {
            var command = new UpdateShipCommand { Id = 0 };

            FluentActions.Invoking(() => SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Test]
        public void ShouldThrowNotFoundExceptionIfIdDoesNotExist()
        {
            var command = UpdateShipCommandBuilder
                .WithValidUpdateShipCommand()
                .With(command => command.Id = int.MaxValue)
                .Build();

            FluentActions.Invoking(() => SendAsync(command)).Should().Throw<NotFoundException>();
        }

        [TestCaseSource(nameof(_invalidCommands))]
        public void ShouldThrowValidationExceptionIfCommandIsInvalid(UpdateShipCommand command) =>
            FluentActions.Invoking(() => SendAsync(command)).Should().Throw<ValidationException>();

        [Test]
        public async Task ShouldThrowDuplicateEntityExceptionIfEntityIsDuplicate()
        {
            await RunAsDefaultUserAsync();
            var ship1 = (await SendAsync(CreateShipCommandBuilder.WithValidCreateShipCommand().Build())).Payload;
            var ship2 = (await SendAsync(CreateShipCommandBuilder.WithValidCreateShipCommand().With(command => command.Code = "BBBB-1111-B1").Build())).Payload;

            var command = UpdateShipCommandBuilder
                .WithValidUpdateShipCommand()
                .With(command => command.Id = ship2.Id)
                .With(command => command.Code = ship1.Code)
                .Build();

            FluentActions.Invoking(() => SendAsync(command))
                .Should().Throw<ValidationException>().Where(ex => ex.Errors.ContainsKey("Code")).And.Errors["Code"].Should().Contain("The specified code already exists.");
        }

        [Test]
        public async Task ShouldUpdateShip()
        {
            var userId = await RunAsDefaultUserAsync();
            var ship = (await SendAsync(CreateShipCommandBuilder.WithValidCreateShipCommand().Build())).Payload;
            var expected = UpdateShipCommandBuilder
                .WithValidUpdateShipCommand()
                .With(command => command.Id = ship.Id)
                .Build();

            await SendAsync(expected);

            var actual = await FindAsync<Ship>(ship.Id);
            actual.Should().NotBeNull();
            actual.Id.Should().Be(expected.Id);
            actual.Name.Should().Be(expected.Name);
            actual.Code.Should().Be(expected.Code);
            actual.LengthInMetres.Should().Be(expected.LengthInMetres);
            actual.WidthInMetres.Should().Be(expected.WidthInMetres);
            actual.RecordStatus.Should().Be(RecordStatus.Active);
            actual.CreatedBy.Should().Be(userId);
            actual.CreatedInUtc.Should().BeCloseTo(ship.CreatedInUtc);
            actual.ModifiedBy.Should().Be(userId);
            actual.ModifiedInUtc.Should().BeCloseTo(DateTime.UtcNow, 10000);
        }
    }
}
