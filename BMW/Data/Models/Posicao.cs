using System.ComponentModel.DataAnnotations;

namespace BMW.Data.Models
{
    public class Posicao
    {
        [Key]
        public int IdPosicao { get; set; }

        [Required, MaxLength(45)]
        public required string Tipo { get; set; }
    }
}
