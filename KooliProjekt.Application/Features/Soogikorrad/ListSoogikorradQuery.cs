using System.Collections.Generic;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Soogikorrad
{
    public class ListSoogikorradQuery : IRequest<OperationResult<IList<Soogikord>>>
    {
    }
}
