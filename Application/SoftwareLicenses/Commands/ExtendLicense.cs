using Application.Core;
using Application.SoftwareLicenses.DTOs;
using MediatR;
using Persistence;

namespace Application.SoftwareServices.Commands;

public class ExtendLicense
{
    public class Command : IRequest<Result<Unit>> {
        public required ExtendLicenseDto Dto { get; set; }
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

            service.ValidTo = service.ValidTo.AddMonths(request.Dto.Months);
            await context.SaveChangesAsync(cancellationToken);

            return Result<Unit>.Success(Unit.Value);
        }
    }
}
