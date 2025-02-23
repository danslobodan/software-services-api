using Domain;
using MediatR;

namespace Application.SoftwareServices.Queries;

public class GetSoftware
{
    public class Query : IRequest<List<Software>> {}

    public class Handler : IRequestHandler<Query, List<Software>>
    {
        public async Task<List<Software>> Handle(Query request, CancellationToken cancellationToken)
        {
            // TODO : get software from CCP

            return Enumerable.Empty<Software>().ToList();
        }
    }
}
