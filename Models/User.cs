using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace UserManager.Models;

public class User : IdentityUser
{
    [Required]
    public DateTime Birthdate { get; set; }

    public User() : base(){}
}
