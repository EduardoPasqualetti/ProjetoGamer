using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Gamer_BancoDeDados.Infra;
using Gamer_BancoDeDados.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Gamer_BancoDeDados.Controllers
{
    [Route("[controller]")]
    public class EquipeController : Controller
    {
        private readonly ILogger<EquipeController> _logger;

        public EquipeController(ILogger<EquipeController> logger)
        {
            _logger = logger;
        }

        //instância do contexto para acessar o banco de dados
        Context c = new Context();

        [Route("Listar")] // https://localhost/Equipe/Listar
        public IActionResult Index()
        {   
            //variável que armazena as equipes listadas do banco de dados
            ViewBag.Equipe = c.Equipe.ToList();
            //retorna a view de equipe (TELA)
            return View();
        }

        [Route("Cadastrar")]
        public IActionResult Cadastrar(IFormCollection form)
        {
            // instancia do objeto equipe
           Equipe novaEquipe = new Equipe();

            // atribuicao de valores recebidos do formulario 
            novaEquipe.Nome = form["Nome"].ToString();

            // Aqui estava chegando como string (nao queremos asim) 
            //  novaEquipe.Imagem = form["Imagem"].ToString();

            // nova logica de upload da imagem
            if (form.Files.Count > 0)
            {
                // variavel file que armazena o arquivo na posicao da imagem, que e 0
                var file = form.Files[0];

                // combinar o diretorio com o novo arquivo que guardara as imagens
                var folder = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot/img/Equipes");
                
                // criar o arquivo, se nao existir
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                // gera o caminho completo ate o caminho do arquivo(imagem - nome com extensao)
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/" , folder, file.FileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                novaEquipe.Imagem = file.FileName;
            }
            else
            {
                // se nao colocar nenhuma imagem, usar uma padrao
                novaEquipe.Imagem = "padrao.png";
            }
            // fim da logica do upload

            // adiciona objeto na tabela do BD
            c.Equipe.Add(novaEquipe);

            // salva alteracoes feitas no BD
            c.SaveChanges();

            // atualiza a lista 
            ViewBag.Equipe = c.Equipe.ToList();

            return LocalRedirect("~/Equipe/Listar");
            
        }

        [Route("Excluir/{id}")]
        public IActionResult Excluir(int id)
        {
             Equipe equipeBuscada = c.Equipe.FirstOrDefault(e=> e.IdEquipe == id);

             c.Remove(equipeBuscada);

             c.SaveChanges();   

            return LocalRedirect("~/Equipe/Listar");
        }

        [Route("Editar/{id}")]
        public IActionResult Editar(int id)
        {
            Equipe equipe = c.Equipe.First(x => x.IdEquipe == id);

            ViewBag.Equipe = equipe;

            return View("Edit");

        }

        [Route("Atualizar")]
        public IActionResult Atualizar(IFormCollection form)
        {
            Equipe equipe = new Equipe();

            equipe.IdEquipe = int.Parse(form["IdEquipe"].ToString());

            equipe.Nome = form["Nome"].ToString();

            if (form.Files.Count > 0)
            {
                var file = form.Files[0];

                var folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/Equipes");

                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                var path = Path.Combine(folder, file.FileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                equipe.Imagem = file.FileName;
            }
            else
            {
                equipe.Imagem = "padrao.png";
            }

            Equipe equipeBuscada = c.Equipe.First(x => x.IdEquipe == equipe.IdEquipe);

            equipeBuscada.Nome = equipe.Nome;
            equipeBuscada.Imagem = equipe.Imagem;

            c.Equipe.Update(equipeBuscada);

            c.SaveChanges();

            return LocalRedirect("~/Equipe/Listar");
        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}