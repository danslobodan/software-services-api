using System;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Customers.Queries;

public class GetCustomers
{
    public class Query : IRequest<List<Customer>> {}

    public class Handler(ApiDbContext context) : IRequestHandler<Query, List<Customer>>
    {
        public async Task<List<Customer>> Handle(Query request, CancellationToken cancellationToken)
        {
            return await context.Customers.ToListAsync(cancellationToken);
        }
    }
}
