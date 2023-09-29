using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CustomerManagement.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public abstract class CustomerBaseController : ControllerBase
{
    private ISender? _mediator;
    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();

}