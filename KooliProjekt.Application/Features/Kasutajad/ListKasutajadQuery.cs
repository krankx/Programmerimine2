using System.Collections.Generic;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Kasutajad
{
    public class ListKasutajadQuery : IRequest<OperationResult<IList<Kasutaja>>>
    {
    }
}
