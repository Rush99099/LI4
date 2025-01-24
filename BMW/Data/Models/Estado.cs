using System.ComponentModel.DataAnnotations;

namespace BMW.Data.Models
{
    public class Estado
    {
        [Key]
        public int IdEstado { get; set; }

        [Required, MaxLength(45)]
        public required string NomeEstado { get; set; }
    }
}
