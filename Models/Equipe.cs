using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Gamer_BancoDeDados.Models
{
    public class Equipe
    {
        //forma de escrita: PascalCase
        //propriedades

        [Key]
        public int IdEquipe { get; set; }

        public string Nome { get; set; }

        public string Imagem { get; set; }

        //referência que a classe Equipe vai ter acesso a coleção Jogador
        public ICollection<Jogador> Jogador { get; set; }
    }
}