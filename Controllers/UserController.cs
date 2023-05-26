using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UserManager.Data.DTO;
using UserManager.Service;

namespace UserManager.Controller;

[ApiController]
[Route("[Controller]")]
public class UserController : ControllerBase
{
    private UserService _userService;
    private TokenService _tokenService;

    public UserController(UserService userService, TokenService tokenService)
    {
        _userService = userService;
        _tokenService = tokenService;
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
        try{
            var token = await _userService.Login(loginDto);

            var response = new {
                Token=token
            };

            return Ok(response);
        }
        catch(Exception err){
            return Unauthorized(err.Message);
        }
    }
}