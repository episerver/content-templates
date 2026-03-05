using System.ComponentModel.DataAnnotations;

namespace Stride.Mvc.Models;

public class LoginViewModel
{
    [Required]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }
}
