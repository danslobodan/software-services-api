using Application.Core;
using Application.SoftwareServices.DTOs;
using MediatR;
using Persistence;

namespace Application.SoftwareServices.Commands;

public class CancelSubscription
{
    public class Command : IRequest<Result<Unit>> {
        public required CancelSubscriptionDto Dto { get; set; }
    }

    public class Handler(ApiDbContext context) : IRequestHandler<Command, Result<Unit>>
    {
        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var service = await context.SoftwareLicenses
                .FindAsync([request.Dto.Id], cancellationToken);

            if (service == null) 
                return Result<Unit>.Failure($"No software license with Id {request.Dto.Id}", 404);

            if (service.AccountId != request.Dto.AccountId)
                return Result<Unit>.Failure($"Account {request.Dto.AccountId} is not the owner of software service {request.Dto.Id}", 403);

            service.State = Domain.LicenseState.Inactive;
            await context.SaveChangesAsync(cancellationToken);

            return Result<Unit>.Success(Unit.Value);
        }
    }
}
