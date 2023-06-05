using Gamer_BancoDeDados.Infra;
using Gamer_BancoDeDados.Models;
using Microsoft.AspNetCore.Mvc;

namespace Gamer_BancoDeDados.Controllers
{
    [Route("[controller]")]
    public class JogadorController : Controller
    {
        private readonly ILogger<JogadorController> _logger;

        public JogadorController(ILogger<JogadorController> logger)
        {
            _logger = logger;
        }

        Context c = new Context();

    
        [Route("Listar")]
        public IActionResult Index()
        {
            ViewBag.Jogador = c.Jogador.ToList();
            ViewBag.Equipe = c.Equipe.ToList();
            
            return View();
        }


        [Route("Cadastrar")]
        public IActionResult Cadastrar(IFormCollection form)
        {
          
           Jogador novoJogador = new Jogador();


            novoJogador.Nome = form["Nome"].ToString();
            novoJogador.Email = form["Email"].ToString();
            novoJogador.Senha = form["Senha"].ToString();


            c.Jogador.Add(novoJogador);

            c.SaveChanges();

            // atualiza a lista 
            ViewBag.Jogador = c.Jogador.ToList();

            return LocalRedirect("~/Jogador/Listar");
            
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}