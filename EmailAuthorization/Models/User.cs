using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EmailAuthorization.Models {
	public class User {
		public string Email { get; set; } = "";
		public string Token { get; set; } = "";
		public bool Activated { get; set; } = false;
		public DateTime TokenDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Hasło nie może być puste")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$", ErrorMessage = "Hasło nie spełnia wymagań")]
		public string Password { get; set; } = "";
		[Compare("Password", ErrorMessage = "Hasła nie są takie same")]
        [Required(ErrorMessage = "Hasło nie może być puste")]
        public string PasswordConfirmation { get; set; } = "";


        public User() { }
		public User(string email, string token, bool activated) {
			Email = email;
			Token = token;
			Activated = activated;
		}
	}
}
