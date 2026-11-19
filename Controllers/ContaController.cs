using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Thi.ProjetoLivraria.Models;

namespace ProjetoLogin.Controllers
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

        // Processa os dados do login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(
            LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Procura o usuario pelo email
            string email = model.Email.Trim();

            var usuario = await _context.Logins
                .FirstOrDefaultAsync(u => u.Email == email);

            // Verifica se o usuario existe
            if (usuario == null ||
                string.IsNullOrWhiteSpace(usuario.SenhaHash))
            {
                ModelState.AddModelError(
                    string.Empty,
                    "E-mail ou senha invalidos."
                );

                return View(model);
            }

            // Confere a senha com o hash armazenado
            var resultado = _hasher.VerifyHashedPassword(
                usuario,
                usuario.SenhaHash,
                model.Senha
            );

            // Se a senha estiver incorreta
            if (resultado == PasswordVerificationResult.Failed)
            {
                ModelState.AddModelError(
                    string.Empty,
                    "E-mail ou senha invalidos."
                );

                return View(model);
            }

            // Atualiza o hash caso seja necessario
            if (resultado == PasswordVerificationResult.SuccessRehashNeeded)
            {
                usuario.SenhaHash = _hasher.HashPassword(
                    usuario,
                    model.Senha
                );

                await _context.SaveChangesAsync();
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