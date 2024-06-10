using System.ComponentModel.DataAnnotations;

namespace ReceitaWSAPI.Models
{
    public class Pedido
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string CNPJ { get; set; }
        public string Resultado { get; set; }
    }
}
