using System.Collections.Generic;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.VererohuMootmised
{
    public class ListVererohuMootmisedQuery : IRequest<OperationResult<IList<VererohuMootmine>>>
    {
    }
}
