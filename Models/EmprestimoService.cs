using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Models
{
    public class EmprestimoService 
    {
        public void Inserir(Emprestimo e)
        {
            using(BibliotecaContext bc = new BibliotecaContext())
            {
                bc.Emprestimos.Add(e);
                bc.SaveChanges();
            }
        }

        public void Atualizar(Emprestimo e)
        {
            using(BibliotecaContext bc = new BibliotecaContext())
            {
                Emprestimo emprestimo = bc.Emprestimos.Find(e.Id);
                emprestimo.NomeUsuario = e.NomeUsuario;
                emprestimo.Telefone = e.Telefone;
                emprestimo.LivroId = e.LivroId;
                emprestimo.DataEmprestimo = e.DataEmprestimo;
                emprestimo.DataDevolucao = e.DataDevolucao;
                emprestimo.Devolvido = e.Devolvido;

                bc.SaveChanges();
            }
        }

        public ICollection<Emprestimo> ListarTodos(FiltrosEmprestimos filtro)
        {
            using(BibliotecaContext bc = new BibliotecaContext())
            {
                //Objeto do tipo query (banco de dados)
                IQueryable<Emprestimo> query;
                
                //Se o filtro for diferente de nulo
                if(filtro != null)
                {
                    //Define dinamicamente a filtragem
                    switch(filtro.TipoFiltro)
                    {
                        //Se for por livro 
                        case "Livro":
                            //Recebe os emprestimos que se encaixam no filtro de livro
                            query = bc.Emprestimos.Where(e => e.Livro.Titulo.Contains(filtro.Filtro));
                        break;

                        //Se for usuário
                        case "Usuário":
                            //Recebe os emprestimos que se encaixam no filtro de usuário
                            query = bc.Emprestimos.Where(e => e.NomeUsuario.Contains(filtro.Filtro));
                        break;

                        //Padrão lista todos sem filtro
                        default:
                            query = bc.Emprestimos;
                        break;
                    }
                }
                //Caso filtro não tenha sido informado
                else
                {
                    //Lista todos sem filtro
                    query = bc.Emprestimos;
                }
                
                //Ordenação padrão (Ordem alfabetica por livro)
                return query.OrderBy(e => e.Livro).ToList();
            }
        }

        public Emprestimo ObterPorId(int id)
        {
            using(BibliotecaContext bc = new BibliotecaContext())
            {
                return bc.Emprestimos.Find(id);
            }
        }
    }
}