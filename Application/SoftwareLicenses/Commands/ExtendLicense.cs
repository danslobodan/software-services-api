using Application.Core;
using MediatR;
using Persistence;

namespace Application.SoftwareServices.Commands;

public class ExtendLicense
{
    public class Command : IRequest<Result<Unit>> {
        public required string Id { get; set; }
        public required string AccountId { get; set; }
        public required int Months { get; set; }
    }

    public class Handler(ApiDbContext context) : IRequestHandler<Command, Result<Unit>>
    {
        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var service = await context.SoftwareLicenses
                .FindAsync([request.Id], cancellationToken);

           if (service == null) 
                return Result<Unit>.Failure($"No software license with Id {request.Id}", 404);

            if (service.AccountId != request.AccountId)
                return Result<Unit>.Failure($"Account {request.AccountId} is not the owner of software service {request.Id}", 403);

            service.ValidTo = service.ValidTo.AddMonths(request.Months);
            await context.SaveChangesAsync(cancellationToken);

            return Result<Unit>.Success(Unit.Value);
        }
    }
}
