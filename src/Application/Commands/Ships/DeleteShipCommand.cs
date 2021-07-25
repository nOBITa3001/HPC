using FluentValidation;
using HPC.Application.Common.Exceptions;
using HPC.Application.Common.Interfaces;
using HPC.Domain.Entities;
using HPC.Domain.Enums;
using HPC.Domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HPC.Application.Commands.Ships
{
    public class DeleteShipCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteShipCommandValidator : AbstractValidator<DeleteShipCommand>
    {
        public DeleteShipCommandValidator()
        {
            RuleFor(command => command.Id)
                .GreaterThan(0);
        }
    }

    public class DeleteShipCommandHandler : IRequestHandler<DeleteShipCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteShipCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteShipCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Ships
                .Where(ship => ship.Id == request.Id)
                .Where(ship => ship.RecordStatus != RecordStatus.Deleted)
                .FirstOrDefaultAsync(cancellationToken);

            if (entity is null)
                throw new NotFoundException(nameof(Ship), request.Id);

            entity.RecordStatus = RecordStatus.Deleted;

            entity.DomainEvents.Add(new ShipDeletedEvent(entity));

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
