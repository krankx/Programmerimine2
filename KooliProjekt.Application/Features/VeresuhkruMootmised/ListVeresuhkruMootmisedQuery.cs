using System.Collections.Generic;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.VeresuhkruMootmised
{
    public class ListVeresuhkruMootmisedQuery : IRequest<OperationResult<IList<VeresuhkruMootmine>>>
    {
    }
}
