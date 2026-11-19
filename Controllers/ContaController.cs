using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Thi.ProjetoLivraria.Models;

namespace Thi.ProjetoLivraria.Controllers
{
    public class ContaController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IPasswordHasher<Login> _hasher;

        public ContaController(
            AppDbContext context,
            IPasswordHasher<Login> hasher)
        {
            _context = context;
            _hasher = hasher;
        }

        // Exibe a tela de login
        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        // Recebe os dados informados pelo usuario
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(
            LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Busca o usuario pelo email
            string email = model.Email.Trim();

            var usuario = await _context.Logins
                .FirstOrDefaultAsync(u => u.Email == email);

            // Verifica se o usuario existe e possui hash
            if (usuario == null ||
                string.IsNullOrWhiteSpace(usuario.SenhaHash))
            {
                ModelState.AddModelError(
                    string.Empty,
                    "E-mail ou senha invalidos."
                );

                return View(model);
            }

            // Verifica a senha utilizando o hash armazenado
            var resultado = _hasher.VerifyHashedPassword(
                usuario,
                usuario.SenhaHash,
                model.Senha
            );

            if (resultado == PasswordVerificationResult.Failed)
            {
                ModelState.AddModelError(
                    string.Empty,
                    "E-mail ou senha invalidos."
                );

                return View(model);
            }

            // Cria a sessao do usuario
            HttpContext.Session.SetString(
                "UsuarioNome",
                usuario.Nome
            );

            // Redireciona para a pagina inicial
            return RedirectToAction("Index", "Home");
        }
    }
}