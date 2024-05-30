using EmailAuthorization.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore.Infrastructure;
using MySqlConnector;
using NuGet.Protocol;

namespace TokenAuthorization.Controllers {

	[Route("EmailAuth/EmailConfirm/{token}")]
	public class EmailAuthController : Controller {
		UserRepository _userRepository = new UserRepository();

        public IActionResult EmailConfirm(string token) {
			var user = _userRepository.getUserByToken(token);
			if(user.Token !="" &&  DateTime.Now - user.TokenDate <= new TimeSpan(7,0,0,0))
			{
				if (!user.Activated)
				{
					ViewBag.ok = "1";	

				}else ViewBag.ok ="0";
			}
			else
			{
				ViewBag.ok = "-1";
			}
			return View(user);
		}

		[HttpPost]
        public IActionResult EmailConfirm(User user) {

            if (ModelState.IsValid)
			{
				_userRepository.activateAccount(user.Token, user.Password);
				ViewBag.ok = "0";

                return View(user);
			}
			else
            {
                ViewBag.ok = "1";
                return View(user);

            }
        }
    }
}
