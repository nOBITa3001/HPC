using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using HPC.Application.Common.Interfaces;
using HPC.Application.Common.Mappings;
using HPC.Application.Common.Models;
using HPC.Application.Dtos.Ships;
using HPC.Application.Queries;
using HPC.Domain.Enums;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HPC.Application.TodoLists.Queries.GetTodos
{
    public class GetShipListQuery : ListQueryBase, IRequest<OperationListResponse<ShipDto>>
    {
        public GetShipListQuery(int page, int size)
            : base(page, size)
        { }
    }

    public class GetShipListQueryValidator : AbstractValidator<GetShipListQuery>
    {
        public GetShipListQueryValidator() =>
            RuleFor(query => query).SetValidator(new ListQueryBaseValidator());
    }

    public class GetShipListQueryHandler : IRequestHandler<GetShipListQuery, OperationListResponse<ShipDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetShipListQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<OperationListResponse<ShipDto>> Handle(GetShipListQuery request, CancellationToken cancellationToken) =>
            await _context.Ships
                .Where(ship => ship.RecordStatus != RecordStatus.Deleted)
                .OrderBy(ship => ship.Name)
                .ProjectTo<ShipDto>(_mapper.ConfigurationProvider)
                .CreateOperationListResponseAsync(request.Page, request.Size);
    }
}
