using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.TodoLists
{
    [ExcludeFromCodeCoverage]
    public class ListToDoListsQuery : IRequest<OperationResult<IList<ToDoList>>>
    {
    }
}
