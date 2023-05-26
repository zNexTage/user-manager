using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace UserManager.Authorization;

public class AgeAuthorization : AuthorizationHandler<MinAge>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinAge requirement)
    {
        var birthdateClaim = context.User.FindFirst(claim => claim.Type == ClaimTypes.DateOfBirth);

        if(birthdateClaim is null) {
            context.Fail();
            return Task.CompletedTask;
        }

        var birthdate = Convert.ToDateTime(birthdateClaim.Value);

        var age = DateTime.Today.Year - birthdate.Year;

        if(birthdate > DateTime.Today.AddYears(-age)){
            age--;
        }

        if(age >= requirement.Age){
            context.Succeed(requirement);
        }

        context.Fail();
        return Task.CompletedTask;
    }
}
