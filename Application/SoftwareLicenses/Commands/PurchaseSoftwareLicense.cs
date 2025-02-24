using Application.Core;
using Application.Interfaces;
using Application.Software.DTOs;
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

    public class Handler(ApiDbContext context, IMapper mapper, ISoftwareVendor vendor) : IRequestHandler<Command, Result<string>>
    {
        public async Task<Result<string>> Handle(Command request, CancellationToken cancellationToken)
        {
            var account = await context.Accounts.FindAsync([request.Dto.AccountId], cancellationToken);
            if (account == null)
                return Result<string>.Failure($"Account {request.Dto.AccountId} does not exist.", 400);

            var orderResult = await vendor.OrderSoftware(new OrderSoftwareRequest() {
                DurationMonths = request.Dto.DurationMonths,
                Id = request.Dto.SoftwareId,
                Quantity = request.Dto.Quantity
            });

            if (!orderResult.Success)
                return Result<string>.Failure($"Failed to order software {request.Dto.SoftwareId}", 500);

            var softwareLicense = mapper.Map<SoftwareLicense>(request.Dto);
            softwareLicense.ValidTo = DateTime.Now.AddMonths(request.Dto.DurationMonths);

            context.SoftwareLicenses.Add(softwareLicense);

            var result = await context.SaveChangesAsync(cancellationToken);

            if (result == 0) return Result<string>.Failure("Failed to create software license", 400);

            return Result<string>.Success(softwareLicense.Id);
        }
    }
}
