using System;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using UserManager.Data.DTO;
using UserManager.Models;

namespace UserManager.Service;

public class CreateUserService
{
    private IMapper _mapper;
    private UserManager<User> _userManager;

    public CreateUserService(IMapper mapper, UserManager<User> userManager)
    {
        this._mapper = mapper;
        this._userManager = userManager;
    }

    public async Task<IdentityResult> execute(CreateUserDTO userDTO){
        User user = _mapper.Map<User>(userDTO);

        /* Using await, we don't have to set the var type to Task<T>; We can set the var with type T  */
        return await _userManager.CreateAsync(user, userDTO.Password);
    }
}
