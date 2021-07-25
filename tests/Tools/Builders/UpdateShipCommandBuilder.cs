using Fare;
using HPC.Application.Commands.Ships;
using HPC.Domain.Constants;
using System;

namespace Tools.Builders
{
    public class UpdateShipCommandBuilder : UpdateShipCommand
    {
        private static readonly Xeger _shipNameGenerator = new("[a-zA-Z]{5} Ship");
        private static readonly Xeger _shipCodeGenerator = new(Constant.Regex.ShipCode);
        private static readonly Random _random = new();

        public static UpdateShipCommandBuilder WithValidUpdateShipCommand() =>
            new()
            {
                Id = _random.Next(1, int.MaxValue),
                Name = _shipNameGenerator.Generate(),
                Code = _shipCodeGenerator.Generate(),
                LengthInMetres = Math.Round((decimal)_random.NextDouble() * 1000, 2),
                WidthInMetres = Math.Round((decimal)_random.NextDouble() * 100, 2)
            };

        public UpdateShipCommandBuilder With(Action<UpdateShipCommandBuilder> action)
        {
            action(this);

            return this;
        }

        public UpdateShipCommand Build()
        {
            return new UpdateShipCommand
            {
                Id = Id,
                Name = Name,
                Code = Code,
                LengthInMetres = LengthInMetres,
                WidthInMetres = WidthInMetres
            };
        }

    }
}
