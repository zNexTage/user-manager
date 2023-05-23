using Microsoft.AspNetCore.Mvc;
using UserManager.Data.DTO;
using UserManager.Service;

namespace UserManager.Controller;

[ApiController]
[Route("[Controller]")]
public class UserController : ControllerBase
{
    private CreateUserService _createUserService;

    public UserController(CreateUserService createUserService)
    {
        _createUserService = createUserService;
    }

    [HttpPost]
    public async Task<IActionResult> Register(CreateUserDTO userDTO){
        
        var result = await _createUserService.execute(userDTO);

        if(result.Succeeded){
            return Ok("Usuário criado com sucesso");
        } 

        return BadRequest(result.Errors);
    }
}