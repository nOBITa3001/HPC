using AutoMapper;
using FluentValidation;
using HPC.Application.Common.Interfaces;
using HPC.Application.Common.Models;
using HPC.Application.Dtos.Ships;
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
    public class CreateShipCommand : IRequest<OperationResponse<ShipDto>>
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public decimal LengthInMetres { get; set; }
        [Required]
        public decimal WidthInMetres { get; set; }
    }

    public class CreateShipCommandValidator : AbstractValidator<CreateShipCommand>
    {
        private readonly IApplicationDbContext _context;

        public CreateShipCommandValidator(IApplicationDbContext context)
        {
            _context = context;

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

        private async Task<bool> BeUniqueCode(CreateShipCommand request, string code, CancellationToken cancellationToken) =>
            await _context.Ships
                .Where(ship => ship.RecordStatus != RecordStatus.Deleted)
                .AllAsync(ship => ship.Code != code, cancellationToken);
    }

    public class CreateShipCommandHandler : IRequestHandler<CreateShipCommand, OperationResponse<ShipDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateShipCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<OperationResponse<ShipDto>> Handle(CreateShipCommand request, CancellationToken cancellationToken)
        {
            var entity = new Ship
            {
                Name = request.Name,
                Code = request.Code,
                LengthInMetres = request.LengthInMetres,
                WidthInMetres = request.WidthInMetres
            };

            entity.DomainEvents.Add(new ShipCreatedEvent(entity));

            _context.Ships.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return new OperationResponse<ShipDto> { Payload = _mapper.Map<ShipDto>(entity) };
        }
    }
}
