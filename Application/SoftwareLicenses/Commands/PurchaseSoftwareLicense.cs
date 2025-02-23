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

            var softwareLicense = mapper.Map<SoftwareLicense>(request.Dto);
            softwareLicense.ValidTo = DateTime.Now.AddMonths(request.Dto.DurationMonths);

            context.SoftwareLicenses.Add(softwareLicense);

            var result = await context.SaveChangesAsync(cancellationToken);

            if (result == 0) return Result<string>.Failure("Failed to create software license", 400);

            return Result<string>.Success(softwareLicense.Id);
        }
    }
}
