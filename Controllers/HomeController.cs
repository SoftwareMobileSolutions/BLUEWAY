using BLUEWAY.Models;
using BLUEWAY.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLUEWAY.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
			var usuario = _Session.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");
			if (usuario == null)
			{
				return await Task.Run(() =>
				{
					return RedirectToAction("Login", "Login");
				});
			} else {
				return await Task.Run(() =>
				{
					return View();
				});
			}
        }
    }
}
