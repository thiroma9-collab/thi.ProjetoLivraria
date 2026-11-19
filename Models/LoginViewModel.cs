using System.ComponentModel.DataAnnotations;

namespace Thi.ProjetoLivraria.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Informe seu e-mail.")]
        [EmailAddress(ErrorMessage = "Digite um e-mail válido.")]
        [StringLength(180)]
        [Display(Name = "E-mail")]
        public string Email { get; set; } = string.Empty;


        [Required(ErrorMessage = "Informe sua senha.")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Senha { get; set; } = string.Empty;
    }
}