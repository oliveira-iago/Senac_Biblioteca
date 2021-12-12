using System.Collections.Generic;
using System.Linq;
using Biblioteca.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace Biblioteca.Controllers
{
    public class Autenticacao
    {
        //Função que verifica o status de login (Se está logado)
        public static void verificaLogin(Controller controller)
        {   
            //Verifica nos cookies se login é nulo ou vazio
            if(string.IsNullOrEmpty(controller.HttpContext.Session.GetString("Login")))
            {
                //Se for nulo ou vazio, manda o usuário para a tela de login
                controller.Request.HttpContext.Response.Redirect("/Home/Login");
            }
        }

        //Função que verifica o login e senha
        public static bool verificaLoginSenha(string login, string senha, Controller controller)
        {
            using (BibliotecaContext bc = new BibliotecaContext())
            {
                //Verifica se o usuario Admin existe
                verificaSeAdminExiste(bc);

                //Criptrografa a senha
                senha = Criptografo.Criptografar(senha);

                //Busca no banco de dados o usuário com o Login e Senha informados
                IQueryable<Usuario> UsuarioEncontrado = bc.Usuarios.Where(u => u.Login==login && u.Senha==senha);
                //Transforma o resultado da busca em uma lista
                List<Usuario> ListaUsuarioEncontrado = UsuarioEncontrado.ToList();

                //Se nenhum usuário for encontrado
                if (ListaUsuarioEncontrado.Count == 0)
                {
                    //Retorna Falso
                    return false;
                }
                //Se for encontrado
                else
                {
                    //Salva as informações nos cookies
                    controller.HttpContext.Session.SetString("Login", ListaUsuarioEncontrado[0].Login);
                    controller.HttpContext.Session.SetString("Nome", ListaUsuarioEncontrado[0].Nome);
                    controller.HttpContext.Session.SetInt32("Tipo", ListaUsuarioEncontrado[0].Tipo);

                    //Retorna verdadeiro
                    return true;    
                }
            }
        }

        //Função que verifica se o usuario Admin existe
        public static void verificaSeAdminExiste(BibliotecaContext bc)
        {
            //Busca usuario com login admin
            IQueryable<Usuario> userEncontrado = bc.Usuarios.Where(u => u.Login == "admin");

            //Se o usuario nao existir
            if (userEncontrado.ToList().Count == 0)
            {   
                //Cria um objeto do tipo usuário
                Usuario admin = new Usuario();

                //Define Login, Senha, Tipo e Nome
                admin.Login = "admin";
                admin.Senha = Criptografo.Criptografar("123"); //Criptografa a senha
                admin.Tipo = Usuario.admin;
                admin.Nome = "Administrador";

                //Adiciona ao banco de dados
                bc.Usuarios.Add(admin);
                //Salva as alterações
                bc.SaveChanges();
            }
        }

        //Função que verifica se o usuário é admin
        public static void verificaSeUsuarioEAdmin(Controller controller)
        {
            //Verifica se o tipo do usuário logado atualmente é diferente do tipo Admin 
            if (controller.HttpContext.Session.GetInt32("Tipo") != Usuario.admin)
            {
                //Se não for admin, redireciona para a pagina "Precisa ser admin"
                controller.Request.HttpContext.Response.Redirect("/Usuario/NeedAdmin");
            }
        }
    }
}