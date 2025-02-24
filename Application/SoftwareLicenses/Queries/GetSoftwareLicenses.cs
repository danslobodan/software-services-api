using Application.Core;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.SoftwareServices.Queries;

public class GetSoftwareLicenses
{
    public class Query : IRequest<Result<List<SoftwareLicense>>> {
        public required string AccountId { get; set; }
    }

    public class Handler(ApiDbContext context) : IRequestHandler<Query, Result<List<SoftwareLicense>>>
    {
        public async Task<Result<List<SoftwareLicense>>> Handle(Query request, CancellationToken cancellationToken)
        {
            // TODO : implement paging
            var services = await context.SoftwareLicenses
                .Where(ss => ss.AccountId == request.AccountId)
                .ToListAsync(cancellationToken);

            return Result<List<SoftwareLicense>>.Success(services);
        }
    }
}
