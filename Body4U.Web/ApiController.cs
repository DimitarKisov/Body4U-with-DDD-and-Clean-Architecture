namespace Body4U.Web
{
    using Body4U.Application.Common;
    using Body4U.Web.Common;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using System.Threading.Tasks;

    [ApiController]
    [Route("[controller]")]
    public class ApiController : ControllerBase
    {
        private IMediator? mediator;

        protected IMediator Mediator
        {
            get
            {
                return this.mediator ??= this.HttpContext
                    .RequestServices
                    .GetService<IMediator>();
            }
        }

        protected Task<ActionResult<TResult>> Send<TResult>(IRequest<TResult> request)
        {
            return this.Mediator.Send(request).ToActionResult();
        }

        protected Task<ActionResult> Send(IRequest<Result> request)
        {
            return this.Mediator.Send(request).ToActionResult();
        }

        protected Task<ActionResult<TResult>> Send<TResult>(IRequest<Result<TResult>> request)
        {
            return this.Mediator.Send(request).ToActionResult();
        }
    }
}
