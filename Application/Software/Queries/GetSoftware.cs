using SoftwareModel = Domain.Software;
using MediatR;
using Application.Interfaces;
using Application.Core;
using Application.Software.DTOs;

namespace Application.Software.Queries;

public class GetSoftware
{
    public class Query : IRequest<Result<List<SoftwareModel>>> {
        public required GetSoftwareDto Dto { get; set; }
    }

    public class Handler(ISoftwareVendor vendor) : IRequestHandler<Query, Result<List<SoftwareModel>>>
    {
        public async Task<Result<List<SoftwareModel>>> Handle(Query query, CancellationToken cancellationToken)
        {
            var page = query.Dto.Page > 0 ? query.Dto.Page : 0;
            var pageSize = query.Dto.PageSize > 0 ? query.Dto.PageSize : 20;

            var software = await vendor.GetSoftware(new GetSoftwareDto() {
                Page = page,
                PageSize = pageSize
            });
            return Result<List<SoftwareModel>>.Success(software);
        }
    }
}
