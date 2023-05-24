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

    public UserService(IMapper mapper,
     UserManager<User> userManager,
     SignInManager<User> signInManager
     )
    {
        this._mapper = mapper;
        this._userManager = userManager;
        this._signInManager = signInManager;
    }

    public async Task<IdentityResult> Create(CreateUserDTO userDTO)
    {
        User user = _mapper.Map<User>(userDTO);

        /* Using await, we don't have to set the var type to Task<T>; We can set the var with type T  */
        return await _userManager.CreateAsync(user, userDTO.Password);
    }

    public async Task<SignInResult> Login(LoginDTO loginDTO)
    {
        var result = await _signInManager.PasswordSignInAsync(
            loginDTO.UserName, 
            loginDTO.Password,
            false, // isPersistent -> we set it to false; We don't want to create a authentication cookie;
            false // lockoutOnFailure -> we set it to false; We dont't want to block the account when the login fail.
        );

        return result;
    }
}
