using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {
        private IMediator _mediator;
        //De este modo reducimos el uso de código en los controladores ya que podremos invocar el Mediator en cualquier
        //controlador hijo ya que este Mediator se encuentra en su padre. El operador ??= lo que hace es que, si el
        //primer argumento (_mediator) es nulo, se le asignará a Mediator el valor de lo que haya a la derecha, en caso
        //contrario -que _mediator no sea null-, Mediator tendrá el valor de _mediator
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    }
}