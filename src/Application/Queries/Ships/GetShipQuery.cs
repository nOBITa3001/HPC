using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using HPC.Application.Common.Exceptions;
using HPC.Application.Common.Interfaces;
using HPC.Application.Common.Mappings;
using HPC.Application.Common.Models;
using HPC.Application.Dtos.Ships;
using HPC.Application.Queries;
using HPC.Domain.Entities;
using HPC.Domain.Enums;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HPC.Application.TodoLists.Queries.GetTodos
{
    public class GetShipQuery : IdQueryBase, IRequest<OperationResponse<ShipDto>>
    { }

    public class GetShipQueryValidator : AbstractValidator<GetShipQuery>
    {
        public GetShipQueryValidator() => RuleFor(query => query.Id).GreaterThan(0);
    }

    public class GetShipQueryHandler : IRequestHandler<GetShipQuery, OperationResponse<ShipDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetShipQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<OperationResponse<ShipDto>> Handle(GetShipQuery request, CancellationToken cancellationToken)
        {
            var result = await _context.Ships
                .Where(ship => ship.Id == request.Id)
                .Where(ship => ship.RecordStatus != RecordStatus.Deleted)
                .ProjectTo<ShipDto>(_mapper.ConfigurationProvider)
                .CreateOperationResponseAsync();

            if (result.Payload is null)
                throw new NotFoundException(nameof(Ship), request.Id);

            return result;
        }
    }
}
