using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gamer_BancoDeDados.Models;
using Microsoft.EntityFrameworkCore;

namespace Gamer_BancoDeDados.Infra
{
    public class Context : DbContext
    {
        public Context()
        {
        }   

        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
            {
                //string de conexão com o banco
                //Data Source é o nome do servidor do gerenciador do banco
                //Initial catalog: Nome do banco de dados

                //Autentificação pelo Windows 
                //Integrated Security: Autenticação pelo Windows 
                //TrustServerCertificate : Autenticação pelo Windows

                //Autentificação pelo SqlServer
                //User Id = "Nome do seu usuário de login"
                //pwd = "Senha do seu usuário"
                optionsBuilder.UseSqlServer("Data Source = NOTE11-S14; Initial Catalog = gamerManha; User Id = sa; pwd = Senai@134; TrustServerCertificate = true");
            }
        }

        //referência de classes e tabelas
        public DbSet<Jogador> Jogador { get; set; }

        public DbSet<Equipe> Equipe { get; set; }


    }
}