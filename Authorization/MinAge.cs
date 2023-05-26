using System;
using Microsoft.AspNetCore.Authorization;

namespace UserManager.Authorization;

public class MinAge : IAuthorizationRequirement
{
    public int Age { get; set; }

    public MinAge(int age)
    {
        this.Age = age;
    }
}
