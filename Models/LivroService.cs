using System.Linq;
using System.Collections.Generic;
using System.Collections;

namespace Biblioteca.Models
{
    public class LivroService
    {
        //Função que insere o livro no banco de dados
        public void Inserir(Livro livro)
        {
            using(BibliotecaContext bc = new BibliotecaContext())
            {
                //Adiciona o livro no banco de dados
                bc.Livros.Add(livro);
                //Salva as alterações
                bc.SaveChanges();
            }
        }

        //Função que atualiza informações de um livro
        public void Atualizar(Livro livro)
        {
            using(BibliotecaContext bc = new BibliotecaContext())
            {   
                //Busca o livro pelo id
                Livro livroAntigo = bc.Livros.Find(livro.Id);
                //Atualiza o Autor
                livroAntigo.Autor = livro.Autor;
                //Atualiza o titulo
                livroAntigo.Titulo = livro.Titulo;
                //Atualiza o ano
                livroAntigo.Ano = livro.Ano;
                
                //Salva as alterações
                bc.SaveChanges();
            }
        }

        //Função que lista todos os livros (filtro padrão é nulo, mas pode ser alterado)
        public ICollection<Livro> ListarTodos(FiltrosLivros filtro = null)
        {
            using(BibliotecaContext bc = new BibliotecaContext())
            {
                //Objeto do tipo query (banco de dados)
                IQueryable<Livro> query;
                
                //Se o filtro for diferente de nulo
                if(filtro != null)
                {
                    //Define dinamicamente a filtragem
                    switch(filtro.TipoFiltro)
                    {
                        //Se for por autor 
                        case "Autor":
                            //Recebe os livros que se encaixam no filtro de autor
                            query = bc.Livros.Where(l => l.Autor.Contains(filtro.Filtro));
                        break;

                        //Se for titulo
                        case "Titulo":
                            //Recebe os livros que se encaixam no filtro de titulo
                            query = bc.Livros.Where(l => l.Titulo.Contains(filtro.Filtro));
                        break;

                        //Padrão lista todos sem filtro
                        default:
                            query = bc.Livros;
                        break;
                    }
                }
                //Caso filtro não tenha sido informado
                else
                {
                    //Lista todos sem filtro
                    query = bc.Livros;
                }
                
                //Ordenação padrão (Ordem alfabetica por titulo)
                return query.OrderBy(l => l.Titulo).ToList();
            }
        }

        //Função que lista os livros disponiveis (que não estão sob emprestimo)
        public ICollection<Livro> ListarDisponiveis()
        {
            using(BibliotecaContext bc = new BibliotecaContext())
            {
                //Busca os livros onde o id não está entre os ids de livros em empréstimo
                //utiliza uma subconsulta
                return
                    bc.Livros
                    .Where(l =>  !(bc.Emprestimos.Where(e => e.Devolvido == false).Select(e => e.LivroId).Contains(l.Id)))
                    .ToList();
            }
        }

        //Função que obtém um livro pelo id
        public Livro ObterPorId(int id)
        {
            using(BibliotecaContext bc = new BibliotecaContext())
            {
                //Retorna o livro que foi encontrado
                return bc.Livros.Find(id);
            }
        }
    }
}