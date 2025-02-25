using Application.Core;
using Application.SoftwareLicenses.DTOs;
using Domain;
using MediatR;
using Persistence;

namespace Application.SoftwareLicenses.Commands;

public class ChangeLicenseQuantity
{
    public class Command : IRequest<Result<Unit>> {
        public required string Id { get; set; }
        public required ChangeLicenseQuantityDto Dto { get; set; }
    }

    public class Handler(ApiDbContext context) : IRequestHandler<Command, Result<Unit>>
    {
        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var service = await context.SoftwareLicenses
                .FindAsync([request.Id], cancellationToken);

            if (service == null) 
                return Result<Unit>.Failure($"No software license with Id {request.Id}", 404);

            if (service.State == LicenseState.Inactive)
                return Result<Unit>.Failure($"Software license {request.Id} is inactive.", 400);

            service.Quantity = request.Dto.Quantity;
            await context.SaveChangesAsync(cancellationToken);   

            return Result<Unit>.Success(Unit.Value);
        }
    }
}
