using Microsoft.AspNetCore.Mvc;

namespace Thi.ProjetoLivraria.Controllers
{
    public class ContaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
