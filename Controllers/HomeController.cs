using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Biblioteca.Models;
using Microsoft.AspNetCore.Http;

namespace Biblioteca.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        //Página inicial
        public IActionResult Index()
        {   
            //Verifica se o usuário está logado (Se nao estiver manda para a pagina de login)
            Autenticacao.verificaLogin(this);

            //Retorna a propria View
            return View();
        }

        //Página de Login
        public IActionResult Login()
        {
            //Retorna a propria view
            return View();
        }

        //Função de quando envia o formulario de login
        [HttpPost]
        public IActionResult Login(string login, string senha)
        {
            //Se o login e senha estiverem corretos
            if(Autenticacao.verificaLoginSenha(login, senha, this))
            {
                //Salva as informações nos cookies
                HttpContext.Session.SetString("Login", login);
                
                //Redireciona para a página inicial
                return RedirectToAction("Index");
            }
            else
            {
                //Exibe a mensagem de erro
                ViewData["Erro"] = "Login ou Senha inválidos";

                //Retorna a propria View
                return View();               
            }
        }

        //Privacy
        public IActionResult Privacy()
        {
            //Retorna a propria View
            return View();
        }
    }
}
