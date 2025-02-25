using Application.Core;
using MediatR;
using Persistence;

namespace Application.SoftwareLicenses.Commands;

public class CancelSubscription
{
    public class Command : IRequest<Result<Unit>> {
        public required string Id { get; set; }
    }

    public class Handler(ApiDbContext context) : IRequestHandler<Command, Result<Unit>>
    {
        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var service = await context.SoftwareLicenses
                .FindAsync([request.Id], cancellationToken);

            if (service == null) 
                return Result<Unit>.Failure($"No software license with Id {request.Id}", 404);

            service.State = Domain.LicenseState.Inactive;
            await context.SaveChangesAsync(cancellationToken);

            return Result<Unit>.Success(Unit.Value);
        }
    }
}
