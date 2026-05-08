using System.Collections.Generic;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.SoogikorraRead
{
    public class ListSoogikorraReadQuery : IRequest<OperationResult<IList<SoogikorraRida>>>
    {
    }
}
