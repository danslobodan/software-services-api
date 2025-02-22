using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Accounts.Queries;

public class GetAccountsList
{
    public class Query : IRequest<List<Account>> {
        public required string CustomerId { get; set; }
    }

    public class Handler(ApiDbContext context) : IRequestHandler<Query, List<Account>>
    {
        public async Task<List<Account>> Handle(Query request, CancellationToken cancellationToken)
        {
            var accounts = await context.Accounts
                .Where(account => account.CustomerId == request.CustomerId)
                .ToListAsync(cancellationToken);

            return accounts;
        }
    }
}
