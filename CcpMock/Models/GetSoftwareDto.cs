using System;

namespace CcpMock.Models;

public class GetSoftwareDto
{
    public required int Page { get; set; }
    public required int PageSize { get; set; }
}
