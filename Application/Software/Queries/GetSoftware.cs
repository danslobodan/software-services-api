using SoftwareModel = Domain.Software;
using MediatR;
using Application.Interfaces;
using Application.Core;

namespace Application.Software.Queries;

public class GetSoftware
{
    public class Query : IRequest<Result<List<SoftwareModel>>> {}

    public class Handler(ISoftwareVendor vendor) : IRequestHandler<Query, Result<List<SoftwareModel>>>
    {
        public async Task<Result<List<SoftwareModel>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var software = await vendor.GetSoftware();
            return Result<List<SoftwareModel>>.Success(software);
        }
    }
}
