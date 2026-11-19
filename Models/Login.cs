using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Thi.ProjetoLivraria.Models
{
    [Table("login")]
    [Index(nameof(Email), IsUnique = true)]
    public class Login
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [StringLength(120)]
        [Column("nome")]
        public string Nome { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [StringLength(180)]
        [Column("email")]
        public string Email { get; set; } = string.Empty;

        [Column("senha")]
        public string? Senha { get; set; }

        [Required]
        [StringLength(255)]
        [Column("senhahash")]
        public string SenhaHash { get; set; } = string.Empty;
    }

}

