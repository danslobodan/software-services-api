using System.Net.Http.Json;
using Application.Interfaces;
using Application.Software.DTOs;
using Domain;

namespace Infrastructure.SoftwareVendors;

public class CcpService(IHttpClientFactory clientFactory) : ISoftwareVendor
{
    readonly HttpClient _client = clientFactory.CreateClient("ccp");

    public async Task<List<Software>> GetSoftware(GetSoftwareDto dto)
    {
        return await _client.GetFromJsonAsync<List<Software>>(
                $"api/software?page={dto.Page}&pageSize={dto.PageSize}") ??
             throw new Exception("Ccp service returned a null result when trying to get software list.");
    }

    public async Task<OrderSofwareResult> OrderSoftware(OrderSoftwareRequest request)
    {
        Console.WriteLine($"{request.Id}");
        var result = await _client.PostAsJsonAsync("api/software", request);

        if (!result.IsSuccessStatusCode) {
            var error = await result.Content.ReadAsStringAsync();
            return new OrderSofwareResult() { Success = false, Error = error };
        }

        return new OrderSofwareResult() { Success = true, Error = string.Empty };
    }
}
