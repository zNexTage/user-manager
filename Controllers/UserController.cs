using Microsoft.AspNetCore.Mvc;
using UserManager.Data.DTO;
using UserManager.Service;

namespace UserManager.Controller;

[ApiController]
[Route("[Controller]")]
public class UserController : ControllerBase
{
    private UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register(CreateUserDTO userDTO){
        
        var result = await _userService.Create(userDTO);

        if(result.Succeeded){
            return Ok("Usuário criado com sucesso");
        } 

        return BadRequest(result.Errors);
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginDTO loginDto){
        var result = await _userService.Login(loginDto);

        if(!result.Succeeded){
            return Unauthorized("Verify your credentials");
        }

        return Ok();
    }
}