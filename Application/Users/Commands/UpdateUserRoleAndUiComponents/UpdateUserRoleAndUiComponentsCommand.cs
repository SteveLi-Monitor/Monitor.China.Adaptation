using Application.Common;
using Application.Common.Interfaces;
using Application.Entities;
using Application.MonitorApis;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Users.Commands.UpdateUserRoleAndUiComponents
{
    public class UpdateUserRoleAndUiComponentsCommand : IValidateRequest<Unit>
    {
        public UpdateUserRoleAndUiComponentsCommand()
        {
            UiComponents = new List<UiComponent>();
        }

        public string Id { get; set; }

        public int? UserRoleId { get; set; }

        public IList<UiComponent> UiComponents { get; set; }


        public bool NeedValidation { get; set; } = true;
    }

    public class UpdateUserRoleAndUiComponentsCommandHandler : IRequestHandler<UpdateUserRoleAndUiComponentsCommand>
    {
        private readonly IApplicationDbContext dbContext;
        private readonly MonitorApiService monitorApiService;

        public UpdateUserRoleAndUiComponentsCommandHandler(
            IApplicationDbContext dbContext,
            MonitorApiService monitorApiService)
        {
            this.dbContext = dbContext;
            this.monitorApiService = monitorApiService;
        }

        public async Task<Unit> Handle(UpdateUserRoleAndUiComponentsCommand request, CancellationToken cancellationToken)
        {
            var applicationUser = await dbContext.ApplicationUsers.FindAsync(request.Id);

            if (applicationUser == null)
            {
                var resp = await monitorApiService.QueryApplicationUsers();
                var user = resp.Users.First(x => x.ApplicationUserId == request.Id);

                await dbContext.ApplicationUsers.AddAsync(
                    new ApplicationUser
                    {
                        Id = user.ApplicationUserId,
                        Username = user.ApplicationUserUsername,
                        UiComponents = request.UiComponents,
                        UserRoleId = request.UserRoleId,
                    });
            }
            else
            {
                applicationUser.UiComponents = request.UiComponents;
                applicationUser.UserRoleId = request.UserRoleId;
            }

            await dbContext.SaveChangesAsync();
            return Unit.Value;
        }
    }
}
