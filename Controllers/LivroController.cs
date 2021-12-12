using Biblioteca.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Biblioteca.Controllers
{
    public class LivroController : Controller
    {
        //Cadastro de livro
        public IActionResult Cadastro()
        {
            //Verifica se o usuário está logado (Se nao estiver manda para a pagina de login)
            Autenticacao.verificaLogin(this);

            //Retorna a propria View
            return View();
        }

        //Função chamada ao enviar o formulario de cadastro de livros
        [HttpPost]
        public IActionResult Cadastro(Livro l)
        {
            //Cria um objeto do tipo LivroService
            LivroService livroService = new LivroService();

            //Se o id do livro for 0
            if(l.Id == 0)
            {
                //Insere o livro no banco de dados
                livroService.Inserir(l);
            }
            else
            {
                //Atualiza as informações no banco de dados
                livroService.Atualizar(l);
            }

            //Redireciona para a listagem de livros
            return RedirectToAction("Listagem");
        }

        //Listagem de livros
        public IActionResult Listagem(string tipoFiltro, string filtro, string itensPorPagina, int numPagina, int paginaAtual)
        {
            //Verifica se o usuário está logado (se não estiver redireciona para login)
            Autenticacao.verificaLogin(this);
            
            //Filtro fica nulo por padrão
            FiltrosLivros objFiltro = null;

            //Se o filtro não estiver nulo
            if(!string.IsNullOrEmpty(filtro))
            {
                //Cria um objeto do tipo FiltrosLivros
                objFiltro = new FiltrosLivros();
                //Define o filtro
                objFiltro.Filtro = filtro;
                //Define o tipo do filtro
                objFiltro.TipoFiltro = tipoFiltro;
            }

            ViewData["livrosPorPagina"] = (string.IsNullOrEmpty(itensPorPagina) ? 10 : Int32.Parse(itensPorPagina)); //Tranforma os itensPorPagina em INT
            ViewData["paginaAtual"] = (paginaAtual !=0 ? paginaAtual : 1); //Se a pagina for diferente de zero, entao é 1

            //Cria um objeto do tipo LivroService
            LivroService livroService = new LivroService();
            
            //Retorna listando apenas os livros que se adequam no filtro
            return View(livroService.ListarTodos(objFiltro));
        }

        //Edição de livro
        public IActionResult Edicao(int id)
        {
            //Verifica se o usuário está logado (se não estiver redireciona para login)
            Autenticacao.verificaLogin(this);

            //Cria um objeto do tipo LivroService
            LivroService livroService = new LivroService();
            //Busca o livro que vai ser editado pelo Id
            Livro livro = livroService.ObterPorId(id);
            //Retorna a propria View, passando o livro que vai ser editado como parametro
            return View(livro);
        }
    }
}