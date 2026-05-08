using System.Collections.Generic;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Toiduained
{
    public class ListToiduainedQuery : IRequest<OperationResult<IList<Toiduaine>>>
    {
    }
}
