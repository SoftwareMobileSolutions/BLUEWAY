using Microsoft.AspNetCore.Mvc;

namespace BLUEWAY.Controllers
{
    public class MapaController : Controller
    {
        public IActionResult mapakml()
        {
            return PartialView();
        }
    }
}
