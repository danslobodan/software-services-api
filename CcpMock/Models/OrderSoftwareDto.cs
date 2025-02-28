namespace CcpMock.Models;

public class OrderSoftwareDto
{
    public required string Id { get; set; }
    public required int Quantity { get; set; }
    public required DateTime ValidTo { get; set; }
}
