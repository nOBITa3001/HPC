using FluentValidation;
using HPC.Application.Common.Exceptions;
using HPC.Application.Common.Interfaces;
using HPC.Domain.Constants;
using HPC.Domain.Entities;
using HPC.Domain.Enums;
using HPC.Domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HPC.Application.Commands.Ships
{
    public class UpdateShipCommand : IRequest
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public decimal LengthInMetres { get; set; }
        [Required]
        public decimal WidthInMetres { get; set; }
    }

    public class UpdateShipCommandValidator : AbstractValidator<UpdateShipCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateShipCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(command => command.Id)
                .GreaterThan(0);

            RuleFor(command => command.Name)
                .NotEmpty()
                .MaximumLength(MaxLengthConfiguration.EntityName);

            RuleFor(command => command.Code)
                .NotEmpty()
                .MaximumLength(MaxLengthConfiguration.ShipCode)
                .Matches(Constant.Regex.ShipCode).WithMessage(Constant.ResponseMessage.InvalidShipCode)
                .MustAsync(BeUniqueCode).WithMessage(Constant.ResponseMessage.DuplicateShipCode);

            RuleFor(command => command.LengthInMetres)
                .GreaterThan(0);

            RuleFor(command => command.WidthInMetres)
                .GreaterThan(0);
        }

        private async Task<bool> BeUniqueCode(UpdateShipCommand request, string code, CancellationToken cancellationToken) =>
            await _context.Ships
                .Where(ship => ship.Id != request.Id)
                .Where(ship => ship.RecordStatus != RecordStatus.Deleted)
                .AllAsync(ship => ship.Code != code, cancellationToken);
    }

    public class UpdateShipCommandHandler : IRequestHandler<UpdateShipCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateShipCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateShipCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Ships
                .Where(ship => ship.Id == request.Id)
                .Where(ship => ship.RecordStatus != RecordStatus.Deleted)
                .FirstOrDefaultAsync(cancellationToken);
            if (entity is null)
                throw new NotFoundException(nameof(Ship), request.Id);

            entity.Name = request.Name;
            entity.Code = request.Code;
            entity.LengthInMetres = request.LengthInMetres;
            entity.WidthInMetres = request.WidthInMetres;

            entity.DomainEvents.Add(new ShipUpdatedEvent(entity));

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
