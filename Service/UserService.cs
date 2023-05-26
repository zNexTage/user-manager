using System;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using UserManager.Data.DTO;
using UserManager.Models;

namespace UserManager.Service;

public class UserService
{
    private IMapper _mapper;
    private UserManager<User> _userManager;
    private SignInManager<User> _signInManager;

    private TokenService _tokenService;

    public UserService(IMapper mapper,
     UserManager<User> userManager,
     SignInManager<User> signInManager,
     TokenService tokenService
     )
    {
        this._mapper = mapper;
        this._userManager = userManager;
        this._signInManager = signInManager;
        this._tokenService = tokenService;
    }

    public async Task<IdentityResult> Create(CreateUserDTO userDTO)
    {
        User user = _mapper.Map<User>(userDTO);

        /* Using await, we don't have to set the var type to Task<T>; We can set the var with type T  */
        return await _userManager.CreateAsync(user, userDTO.Password);
    }

    public async Task<string> Login(LoginDTO loginDTO)
    {
        var result = await _signInManager.PasswordSignInAsync(
            loginDTO.UserName, 
            loginDTO.Password,
            false, // isPersistent -> we set it to false; We don't want to create a authentication cookie;
            false // lockoutOnFailure -> we set it to false; We dont't want to block the account when the login fail.
        );

        if(!result.Succeeded){
            throw new Exception("Não foi possível realizar o login! Verifique as credenciais informadas.");
        }

        var user = _signInManager.UserManager.Users.FirstOrDefault(user => 
        user.NormalizedUserName == loginDTO.NormalizedUserName)!;

        return _tokenService.GenerateToken(user);
    }
}
