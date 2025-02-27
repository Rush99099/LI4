using System.ComponentModel.DataAnnotations;
namespace BMW.Data.Models
{
    public class Utilizador
    {
        [Key]
        public int IdUtilizador { get; set; }
        public string Email { get; set; }
        public string Nome { get; set; }
        public string Password { get; set; }
        public bool IsCliente { get; set; }

        public Utilizador()
        {
            Email = String.Empty;
            Nome = String.Empty;
            Password = String.Empty;
            IsCliente = true;
        }
        // Construtor parametrizado
        public Utilizador(string email, string nome, string password, bool isClient)
        {
            Email = email;
            Nome = nome;
            Password = password;
            IsCliente = isClient;
        }
        public Utilizador(int id, string email, string nome, string password, bool isClient)
        {
            IdUtilizador = id;
            Email = email;
            Nome = nome;
            Password = password;
            IsCliente = isClient;
        }
    }

}
