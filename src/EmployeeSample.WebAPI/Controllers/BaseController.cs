using EmployeeSample.Infrastructure.Api.Response;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeSample.WebAPI.Controllers;

[ApiController]
[Route("v1/[controller]")]
public abstract class BaseController : ControllerBase
{
    protected virtual IActionResult Success(object? result, bool created = false)
    {
        if (created)
            return Created("", new ResponseModel(true, result!));

        return Ok(new ResponseModel(true, result!));
    }
}
