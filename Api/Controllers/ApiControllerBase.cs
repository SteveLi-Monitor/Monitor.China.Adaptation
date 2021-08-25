using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Controllers
{
    public abstract class ApiControllerBase : ControllerBase
    {
        private ISender sender;

        protected ISender Mediator
        {
            get
            {
                if (sender == null)
                {
                    sender = HttpContext.RequestServices.GetRequiredService<ISender>();
                }

                return sender;
            }
        }
    }
}
