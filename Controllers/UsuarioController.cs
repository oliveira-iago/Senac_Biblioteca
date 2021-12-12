using Biblioteca.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;

namespace Biblioteca.Controllers
{
    public class UsuarioController : Controller
    {
        //Página de listagem dos usuarios
        public IActionResult ListaUsuarios()
        {   
            //Verifica se usuario está logado
            Autenticacao.verificaLogin(this);
            //Verifica se usuário é admin
            Autenticacao.verificaSeUsuarioEAdmin(this);

            //Retorna a view passando a lista como parametro
            return View(new UsuarioService().Listar());
        }

        //Página que insere um usuario
        public IActionResult CadastrarUsuario()
        {
            //Verifica se usuario está logado
            Autenticacao.verificaLogin(this);
            //Verifica se usuário é admin
            Autenticacao.verificaSeUsuarioEAdmin(this);

            //Retorna a propria view
            return View();
        }

        //Função de quando o formulario de cadastro de usuarios é enviado
        [HttpPost]
        public IActionResult CadastrarUsuario(Usuario novoUsuario)
        {
            //Criptografa a senha do usuario
            novoUsuario.Senha = Criptografo.Criptografar(novoUsuario.Senha);
            
            //Adiciona o usuario no banco de dados
            new UsuarioService().adicionarUsuario(novoUsuario);

            //Retorna a página de cadastro realizado
            return RedirectToAction("CadastroRealizado");
        }

        //Página de cadastro realizado
        public IActionResult CadastroRealizado()
        {
            //Retorna a propria View
            return View();
        }

        //Página de edição de usuario
        public IActionResult EditarUsuario(int id)
        {
            //Verifica o usuario logado
            Autenticacao.verificaLogin(this);
            //Verifica se usuário é admin
            Autenticacao.verificaSeUsuarioEAdmin(this);

            //Busca o usuario que vai ser editado pelo id
            Usuario usuario = new UsuarioService().Buscar(id);
            
            //Retorna a view com o usuario como parametro
            return View(usuario);
        }

        //Função de quando o formulario de edição de usuarios é enviado
        [HttpPost]
        public IActionResult EditarUsuario(Usuario usuarioEditado)
        {
            //Edita o usuario no banco de dados
            new UsuarioService().editarUsuario(usuarioEditado);

            //Redireciona para a página de listagem de usuarios
            return RedirectToAction("ListaUsuarios");
        }

        //Página de exclusão de usuário
        public IActionResult ExcluirUsuario(int id)
        {
            //Verifica o usuario logado
            Autenticacao.verificaLogin(this);
            //Verifica se usuário é admin
            Autenticacao.verificaSeUsuarioEAdmin(this);
            //Retorna a view passando o usuario que vai ser excluido como parametro
            return View(new UsuarioService().Buscar(id));
        }

        //Função de quando o formulario de exclusão de usuarios é enviado
        [HttpPost]
        public IActionResult ExcluirUsuario(string decisao, int id)
        {
            //Se o usuario confirmou que quer excluir
            if(decisao == "EXCLUIR")
            {
                //Exibe a mensagem
                ViewData["Mensagem"] = "Exclusão do usuário " + new UsuarioService().Buscar(id).Nome + " realizada";

                //Exclui o usuario do banco de dados
                new UsuarioService().excluirUsuario(id);

                //Retorna para a View de Listagem de usuarios, passando a lista de usuarios atualizada como parametro
                return View("ListaUsuarios", new UsuarioService().Listar());
            }
            //Senão
            else
            {
                //Exibe a mensagem
                ViewData["Mensagem"] = "Exclusão Cancelada";
                //Retorna para a View de Listagem de usuarios, passando a lista de usuarios atualizada como parametro
                return View("ListaUsuarios", new UsuarioService().Listar());
            }
        }

        //Função que faz logoff
        public IActionResult Sair()
        {
            //Limpa as informações nos cookies
            HttpContext.Session.Clear();
            //Retorna para a página de login
            return RedirectToAction("Index", "Home");
        }

        //Página NeedAdmin
        public IActionResult NeedAdmin()
        {
            //Verifica o usuario logado
            Autenticacao.verificaLogin(this);
            //Retorna a propria view
            return View();
        }
    }
}