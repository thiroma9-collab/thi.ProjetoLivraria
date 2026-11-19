using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Thi.ProjetoLivraria.Models
{
    [Table("livro")]
    public class Livro
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe o ISBN do livro.")]
        [StringLength(20)]
        [Column("isbn")]
        public string Isbn { get; set; } = string.Empty;

        [Required(ErrorMessage = "Informe o nome do livro.")]
        [StringLength(180)]
        [Column("nome")]
        public string Nome { get; set; } = string.Empty;

        [StringLength(2000)]
        [Column("descricao")]
        public string? Descricao { get; set; }

        [Range(typeof(decimal), "0.01", "99999999.99",
            ErrorMessage = "Informe um valor válido para o livro.")]
        [Column("valor", TypeName = "decimal(10,2)")]
        public decimal Valor { get; set; }
    }
}
