using System;
using System.ComponentModel.DataAnnotations;

namespace UserManager.Data.DTO;

public class CreateUserDTO
{
    [Required]
    public string Username { get; set; }
    
    [Required]
    [DataType(DataType.DateTime)]
    public DateTime Birthdate {get;set;}
    
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Required]
    [Compare("Password")]
    public string RePassword { get; set; }
}
