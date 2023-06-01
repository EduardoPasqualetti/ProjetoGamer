using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gamer_BancoDeDados.Models
{
    public class Jogador
    {
        [Key]//DataAnnotion
        public int IdJogador { get; set; }

        public string Nome { get; set; }

        public string Email { get; set; }

        public string Senha { get; set; }

        //referência da chave estrangeira na tabela equipe
        [ForeignKey("Equipe")] // DataAnnotion
        public int IdEquipe { get; set; }
        public Equipe Equipe { get; set; }
    }
}