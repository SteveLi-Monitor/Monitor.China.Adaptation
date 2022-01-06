using Application.Common.Interfaces;
using Application.Users.Queries.GetById;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Users.Queries.GetUiComponentsById
{
    public class GetUiComponentsByIdQuery : IValidateRequest<GetUiComponentsByIdQueryResp>
    {
        public string Id { get; set; }

        public bool NeedValidation { get; set; } = true;
    }

    public class GetUiComponentsByIdQueryHandler : IRequestHandler<GetUiComponentsByIdQuery, GetUiComponentsByIdQueryResp>
    {
        private readonly ISender mediator;

        public GetUiComponentsByIdQueryHandler(ISender mediator)
        {
            this.mediator = mediator;
        }

        public async Task<GetUiComponentsByIdQueryResp> Handle(GetUiComponentsByIdQuery request, CancellationToken cancellationToken)
        {
            var resp = new GetUiComponentsByIdQueryResp();

            var getByIdQueryResp = await mediator.Send(new GetByIdQuery { Id = request.Id });

            if (getByIdQueryResp.User.UserRole != null)
            {
                resp.UiComponents = getByIdQueryResp.User.UserRole.UiComponents;
            }
            if (getByIdQueryResp.User.UiComponents.Any())
            {
                resp.UiComponents = getByIdQueryResp.User.UiComponents;
            }

            return resp;
        }
    }
}
