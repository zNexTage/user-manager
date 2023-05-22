using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UserManager.Models;

namespace UserManager.Data;

public class UserDbContext : IdentityDbContext<User>{
    public UserDbContext(DbContextOptions options) : base(options)
    {
        
    }
}