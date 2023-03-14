using BLUEWAY.Interfaces;
using BLUEWAY.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLUEWAY.Util;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace BLUEWAY.Controllers
{
    public class LoginController : Controller
    {
		private readonly ILogin ILogin;
		public LoginController(ILogin _ILogin) {
			ILogin = _ILogin;
		}


		public IActionResult Login()
		{
			var usuario = _Session.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");
			if (usuario != null)
			{
				return RedirectToAction("Index", "Home");
			}
            return PartialView();
		}

		[HttpPost]
		//[ValidateAntiForgeryToken]
		public async Task<JsonResult> IniciarSesion(string usuario, string password)
		{
			_Convert _Convert = new _Convert();
			var data = await ILogin.authlogin(usuario, (_Convert.SHA1HashStringForUTF8String(password)).ToUpper());
			return await Task.Run(() =>
			{
				if (data.Where(d => d.userid != 0).Count() > 0)
				{
					List<string> tiposplan = new List<string>(data.Select(s => s.tipoplan).ToList()[0].Split(','));
					var ndata = data.Select(x => {
						x.tipoplan = tiposplan[0];
						return x;
					}).ToList();
					_Session.Set(HttpContext.Session, "usuario", ndata);
					return Json(true);
				} else
				{
					return Json(false);
				}
			});
		}

		[HttpGet]
		public JsonResult CerrarSesion()
		{
			var requiredKeys = HttpContext.Session.Keys;

			foreach (var cookieKey in HttpContext.Request.Cookies.Keys)
			{
				HttpContext.Response.Cookies.Delete(cookieKey);
			}

			foreach (var key in requiredKeys)
			{
				HttpContext.Session.SetString(key, JsonConvert.SerializeObject(null));
			}
			return Json(true);
		}

		
    }
}
