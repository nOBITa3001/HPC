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

namespace HPC.Application.IntegrationTests.Commands.Ships
{
    public class CreateShipTests : TestBase
    {
        static readonly CreateShipCommand[] _invalidCommands =
        {
            CreateShipCommandBuilder
                .WithValidCreateShipCommand()
                .With(builder => builder.Name = string.Empty)
                .Build(),
            CreateShipCommandBuilder
                .WithValidCreateShipCommand()
                .With(builder => builder.Name = new string('A', 201))
                .Build(),
            CreateShipCommandBuilder
                .WithValidCreateShipCommand()
                .With(builder => builder.Code = string.Empty)
                .Build(),
            CreateShipCommandBuilder
                .WithValidCreateShipCommand()
                .With(builder => builder.Code = "Invalid Code")
                .Build(),
            CreateShipCommandBuilder
                .WithValidCreateShipCommand()
                .With(builder => builder.LengthInMetres = 0)
                .Build(),
            CreateShipCommandBuilder
                .WithValidCreateShipCommand()
                .With(builder => builder.WidthInMetres = 0)
                .Build()
        };

        [TestCaseSource(nameof(_invalidCommands))]
        public void ShouldThrowValidationExceptionIfCommandIsInvalid(CreateShipCommand command) =>
            FluentActions.Invoking(() => SendAsync(command)).Should().Throw<ValidationException>();

        [Test]
        public async Task ShouldThrowDuplicateEntityExceptionIfEntityIsDuplicate()
        {
            await RunAsDefaultUserAsync();
            var createdShip = (await SendAsync(CreateShipCommandBuilder.WithValidCreateShipCommand().Build())).Payload;

            FluentActions.Invoking
            (
                () => SendAsync(CreateShipCommandBuilder.WithValidCreateShipCommand().With(command => command.Code = createdShip.Code).Build())
            )
            .Should().Throw<ValidationException>().Where(ex => ex.Errors.ContainsKey("Code"))
            .And.Errors["Code"].Should().Contain("The specified code already exists.");
        }

        [Test]
        public async Task ShouldCreateShip()
        {
            var userId = await RunAsDefaultUserAsync();
            
            var expected = (await SendAsync(CreateShipCommandBuilder.WithValidCreateShipCommand().Build())).Payload;

            var actual = await FindAsync<Ship>(expected.Id);
            actual.Should().NotBeNull();
            actual.Id.Should().Be(expected.Id);
            actual.Name.Should().Be(expected.Name);
            actual.Code.Should().Be(expected.Code);
            actual.LengthInMetres.Should().Be(expected.LengthInMetres);
            actual.WidthInMetres.Should().Be(expected.WidthInMetres);
            actual.RecordStatus.Should().Be(RecordStatus.Active);
            actual.CreatedBy.Should().Be(userId);
            actual.CreatedInUtc.Should().BeCloseTo(DateTime.UtcNow, 10000);
            actual.ModifiedBy.Should().Be(userId);
            actual.ModifiedInUtc.Should().BeCloseTo(DateTime.UtcNow, 10000);
        }
    }
}
