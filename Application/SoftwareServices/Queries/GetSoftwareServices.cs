using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.SoftwareServices.Queries;

public class GetSoftwareServices
{
    public class Query : IRequest<List<SoftwareService>> {
        public required string AccountId { get; set; }
    }

    public class Handler(ApiDbContext context) : IRequestHandler<Query, List<SoftwareService>>
    {
        public async Task<List<SoftwareService>> Handle(Query request, CancellationToken cancellationToken)
        {
            var services = await context.SoftwareServices
                .Where(ss => ss.AccountId == request.AccountId)
                .ToListAsync(cancellationToken);

            return services;
        }
    }
}
