using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Accounts.Queries;

public class GetAccounts
{
    public class Query : IRequest<List<Account>> {}

    public class Handler(ApiDbContext context) : IRequestHandler<Query, List<Account>>
    {
        public async Task<List<Account>> Handle(Query request, CancellationToken cancellationToken)
        {
            var accounts = await context.Accounts
                .ToListAsync(cancellationToken);

            return accounts;
        }
    }
}
