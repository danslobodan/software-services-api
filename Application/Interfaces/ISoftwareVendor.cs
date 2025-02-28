using Application.Software.DTOs;
using SoftwareModel = Domain.Software;

namespace Application.Interfaces;

public interface ISoftwareVendor
{
    Task<List<SoftwareModel>> GetSoftware(GetSoftwareDto dto);
    Task<OrderSofwareResult> OrderSoftware(OrderSoftwareRequest request);
}
