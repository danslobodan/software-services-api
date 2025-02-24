using Application.Core;
using Application.SoftwareServices.DTOs;
using AutoMapper;
using Domain;
using MediatR;
using Persistence;

namespace Application.SoftwareServices.Commands;

public class PurchaseSoftwareLicense
{
    public class Command : IRequest<Result<string>> {
        public required PurchaseSoftwareLicenseDto Dto { get; set; }
    }

    public class Handler(ApiDbContext context, IMapper mapper) : IRequestHandler<Command, Result<string>>
    {
        public async Task<Result<string>> Handle(Command request, CancellationToken cancellationToken)
        {
            // TODO : purchase software from CCP, API call

            var account = await context.Accounts.FindAsync([request.Dto.AccountId], cancellationToken);
            if (account == null)
                return Result<string>.Failure($"Account {request.Dto.AccountId} does not exist.", 400);

            var softwareLicense = mapper.Map<SoftwareLicense>(request.Dto);
            softwareLicense.ValidTo = DateTime.Now.AddMonths(request.Dto.DurationMonths);

            context.SoftwareLicenses.Add(softwareLicense);

            var result = await context.SaveChangesAsync(cancellationToken);

            if (result == 0) return Result<string>.Failure("Failed to create software license", 400);

            return Result<string>.Success(softwareLicense.Id);
        }
    }
}
