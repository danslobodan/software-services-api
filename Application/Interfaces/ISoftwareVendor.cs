using Application.Software.DTOs;
using SoftwareModel = Domain.Software;

namespace Application.Interfaces;

public interface ISoftwareVendor
{
    Task<List<SoftwareModel>> GetSoftware();
    Task<OrderSofwareResult> OrderSoftware(OrderSoftwareRequest request);
}
