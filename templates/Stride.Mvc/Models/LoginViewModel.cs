using System.ComponentModel.DataAnnotations;

namespace Stride.Mvc._1.Models;

public class LoginViewModel
{
    [Required]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }
}
