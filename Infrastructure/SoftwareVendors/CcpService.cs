using Application.Interfaces;
using Application.Software.DTOs;
using Domain;
using Infrastructure.SoftwareVendors;
using Microsoft.Extensions.Options;

namespace Infrastructure;

public class CcpService : ISoftwareVendor
{
    public CcpService(IOptions<CcpSettings> config)
    {
        // TODO : prepare the authorization header
    }

    public Task<List<Software>> GetSoftware()
    {
        throw new NotImplementedException();
    }

    public Task<OrderSofwareResult> OrderSoftware(OrderSoftwareRequest request)
    {
        throw new NotImplementedException();
    }
}
