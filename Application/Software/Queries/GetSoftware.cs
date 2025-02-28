using SoftwareModel = Domain.Software;
using MediatR;
using Application.Interfaces;
using Application.Core;
using Application.Software.DTOs;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Application.Software.Queries;

public class GetSoftware
{
    public class Query : IRequest<Result<List<SoftwareModel>>> {
        public required GetSoftwareDto Dto { get; set; }
    }

    public class Handler(ISoftwareVendor vendor, IDistributedCache cache) : IRequestHandler<Query, Result<List<SoftwareModel>>>
    {
        public async Task<Result<List<SoftwareModel>>> Handle(Query query, CancellationToken cancellationToken)
        {
            var page = query.Dto.Page > 0 ? query.Dto.Page : 0;
            var pageSize = query.Dto.PageSize > 0 ? query.Dto.PageSize : 20;    

            var cacheKey = $"software-services-{page}-{pageSize}";
            var cachedJson = await cache.GetStringAsync(cacheKey, cancellationToken);

            if (cachedJson != null) {
                var cached = JsonSerializer.Deserialize<List<SoftwareModel>>(cachedJson)
                    ?? throw new Exception("Cannot deserialize Software");

                Console.WriteLine($"Returned from cache {cachedJson}");
                return Result<List<SoftwareModel>>.Success(cached);
            }

            var software = await vendor.GetSoftware(new GetSoftwareDto() {
                Page = page,
                PageSize = pageSize
            });

            await cache.SetStringAsync(
                cacheKey,
                JsonSerializer.Serialize(software),
                cancellationToken
            );

            Console.WriteLine("Returned from database");

            return Result<List<SoftwareModel>>.Success(software);
        }
    }
}
