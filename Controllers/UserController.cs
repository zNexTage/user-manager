using System;
using Microsoft.AspNetCore.Mvc;
using UserManager.Data.DTO;

namespace UserManager.Controller;

[ApiController]
[Route("[Controller]")]
public class UserController : ControllerBase
{
    [HttpPost]
    public IActionResult Register(CreateUserDTO userDTO){
        throw new NotImplementedException();
    }
}