using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppMvc.Models
{
	public class Aluno
	{
		[Key]
		public int Id { get; set; }

		[Required(ErrorMessage = "O campo {0} é obrigatório")]
		[StringLength(30, MinimumLength = 2, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres")]
		public string Nome { get; set; }

		[Required(ErrorMessage = "O campo {0} é obrigatório")]
		[DataType(DataType.Date, ErrorMessage ="O campo {0} está em formato incorreto")]
		[Display(Name = "Data de nascimento")]
		public DateTime DataNascimento { get; set; }

		[Required(ErrorMessage = "O campo {0} é obrigatório")]
		[StringLength(100, ErrorMessage = "0 campo {0} precisa ter no máximo {1} caracteres")]
		[RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$",ErrorMessage = "O {0} está em formato inválido. Válido (exemplo@dominio.com).")]
		public string Email { get; set; }

		[Required(ErrorMessage = "O campo {0} é obrigatório")]
		[Display(Name = "Confirme o e-mail")]
		[Compare("Email", ErrorMessage = "Os emails não coincidem.")]
		[NotMapped]
		public string EmailConfirmacao { get; set; }

		[Required(ErrorMessage = "O campo {0} é obrigatório")]
		[Range(1, 5, ErrorMessage = "A avaliação deve estar entre 1 e 5.")]
		public int Avaliacao { get; set; }
		public bool Ativo { get; set; }
	}

}
