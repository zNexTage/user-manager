using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[Controller]")]
public class AccessController : ControllerBase
{
    [HttpGet]
    [Authorize(Policy = "MinAge")]
    public IActionResult Get(){
        return Ok("Acesso permitido");
    }
}
