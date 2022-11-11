using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Mailfa.CleanArch.Controllers
{
[ApiController]
[ApiVersion("1")]
[Route("api/[controller]/[action]")]
public abstract class BaseApiController<T> : Controller
{
    private ILogger<T> _loggerInstance;

    protected ILogger<T> Logger => _loggerInstance ??= HttpContext.RequestServices.GetService<ILogger<T>>();
}
}