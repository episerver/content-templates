using System.ComponentModel.DataAnnotations;

namespace Alloy.Mvc.Setup
{
    /// <summary>
    /// Defines the model for a admin user registration.
    /// </summary>
    public class RegisterAdminUserViewModel
    {
        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        [Required]
        [Display(Name = "Username")]
        [RegularExpression(@"^[a-zA-Z0-9_-]+$", ErrorMessage = "Username can only contain letters a-z, numbers, underscores and hyphens.")]
        [StringLength(20, ErrorMessage = "The {0} field can not be more than {1} characters long.")]
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the email address.
        /// </summary>
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the password confirmation.
        /// </summary>
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
