using Fare;
using HPC.Application.Commands.Ships;
using HPC.Domain.Constants;
using System;

namespace Tools.Builders
{
    public class CreateShipCommandBuilder : CreateShipCommand
    {
        private static readonly Xeger _shipNameGenerator = new("[a-zA-Z]{5} Ship");
        private static readonly Xeger _shipCodeGenerator = new(Constant.Regex.ShipCode);
        private static readonly Random _random = new();

        public static CreateShipCommandBuilder WithValidCreateShipCommand() =>
            new()
            {
                Name = _shipNameGenerator.Generate(),
                Code = _shipCodeGenerator.Generate(),
                LengthInMetres = Math.Round((decimal)_random.NextDouble() * 1000, 2),
                WidthInMetres = Math.Round((decimal)_random.NextDouble() * 100, 2)
            };

        public CreateShipCommandBuilder With(Action<CreateShipCommandBuilder> action)
        {
            action(this);

            return this;
        }

        public CreateShipCommand Build()
        {
            return new CreateShipCommand
            {
                Name = Name,
                Code = Code,
                LengthInMetres = LengthInMetres,
                WidthInMetres = WidthInMetres
            };
        }

    }
}
