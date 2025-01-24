namespace BMW.Data.Models
{
    public class Utilizador
    {
        public int IdUtilizador { get; set; }
        public string Email { get; set; }
        public string Nome { get; set; }
        public string Password { get; set; }
        public bool IsClient { get; set; }

        // Construtor parametrizado
        public Utilizador(int id, string email, string nome, string password, bool isClient)
        {
            IdUtilizador = id;
            Email = email;
            Nome = nome;
            Password = password;
            IsClient = isClient;
        }
    }

}
