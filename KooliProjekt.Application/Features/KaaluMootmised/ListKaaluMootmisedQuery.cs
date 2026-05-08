using System.Collections.Generic;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.KaaluMootmised
{
    public class ListKaaluMootmisedQuery : IRequest<OperationResult<IList<KaaluMootmine>>>
    {
    }
}
