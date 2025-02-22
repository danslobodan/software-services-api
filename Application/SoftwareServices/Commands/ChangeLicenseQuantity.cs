using MediatR;
using Persistence;

namespace Application.SoftwareServices.Commands;

public class ChangeLicenseQuantity
{
    public class Command : IRequest {
        public required string Id { get; set; }
        public required string AccountId { get; set; }
        public required int Quantity { get; set; }
    }

    public class Handler(ApiDbContext context) : IRequestHandler<Command>
    {
        public async Task Handle(Command request, CancellationToken cancellationToken)
        {
            var service = await context.SoftwareServices
                .FindAsync([request.Id], cancellationToken)
                ?? throw new Exception($"Cannot find software with Id {request.Id}");

            if (service.AccountId != request.AccountId)
                throw new Exception($"Account {request.AccountId} is not the owner of software service {request.Id}");

            service.Quantity = request.Quantity;
            await context.SaveChangesAsync(cancellationToken);   
        }
    }
}
