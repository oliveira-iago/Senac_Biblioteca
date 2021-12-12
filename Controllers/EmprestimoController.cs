using Biblioteca.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;

namespace Biblioteca.Controllers
{
    
    public class EmprestimoController : Controller
    {
        public IActionResult Cadastro()
        {
            //Verifica se o usuário está logado (Se nao estiver manda para a pagina de login)
            Autenticacao.verificaLogin(this);

            LivroService livroService = new LivroService();
            EmprestimoService emprestimoService = new EmprestimoService();
            CadEmprestimoViewModel cadModel = new CadEmprestimoViewModel();

            //Lista apenas os livros disponiveis
            cadModel.Livros = livroService.ListarDisponiveis();
            
            return View(cadModel);
        }

        [HttpPost]
        public IActionResult Cadastro(CadEmprestimoViewModel viewModel)
        {
            EmprestimoService emprestimoService = new EmprestimoService();
            
            if(viewModel.Emprestimo.Id == 0)
            {
                emprestimoService.Inserir(viewModel.Emprestimo);
            }
            else
            {
                emprestimoService.Atualizar(viewModel.Emprestimo);
            }
            return RedirectToAction("Listagem");
        }

        public IActionResult Listagem(string tipoFiltro, string filtro)
        {
            //Verifica se o usuário está logado (Se nao estiver manda para a pagina de login)
            Autenticacao.verificaLogin(this);

            //Filtro fica nulo por padrão
            FiltrosEmprestimos objFiltro = null;
            
            //Se o filtro não estiver nulo
            if(!string.IsNullOrEmpty(filtro))
            {
                //Cria um objeto do tipo FiltrosEmprestimos
                objFiltro = new FiltrosEmprestimos();
                //Define o filtro
                objFiltro.Filtro = filtro;
                //Define o tipo do filtro
                objFiltro.TipoFiltro = tipoFiltro;
            }
            
            //Cria um objeto do tipo EmprestimoService
            EmprestimoService emprestimoService = new EmprestimoService();
             
            //Retorna listando apenas os emprestimos que se adequam no filtro
            return View(emprestimoService.ListarTodos(objFiltro));
        }

        public IActionResult Edicao(int id)
        {
            //Verifica se o usuário está logado (Se nao estiver manda para a pagina de login)
            Autenticacao.verificaLogin(this);
            
            LivroService livroService = new LivroService();
            EmprestimoService em = new EmprestimoService();
            Emprestimo e = em.ObterPorId(id);

            CadEmprestimoViewModel cadModel = new CadEmprestimoViewModel();
            cadModel.Livros = livroService.ListarTodos();
            cadModel.Emprestimo = e;
            
            return View(cadModel);
        }
    }
}