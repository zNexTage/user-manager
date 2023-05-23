using System;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UserManager.Data.DTO;
using UserManager.Models;

namespace UserManager.Controller;

[ApiController]
[Route("[Controller]")]
public class UserController : ControllerBase
{
    private IMapper _mapper;
    private UserManager<User> _userManager;

    public UserController(IMapper mapper, UserManager<User> userManager)
    {
        this._mapper = mapper;
        this._userManager = userManager;
    }

    [HttpPost]
    public async Task<IActionResult> Register(CreateUserDTO userDTO){
        User user = _mapper.Map<User>(userDTO);

        /* Using await, we don't have to set the var type to Task<T>; We can set the var with type T  */
        IdentityResult result = await _userManager.CreateAsync(user, userDTO.Password);

        if(result.Succeeded){
            return Ok("Usuário cadastrado com sucesso");
        }

        return BadRequest(result.Errors);
    }
}