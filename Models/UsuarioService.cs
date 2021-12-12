using System.Collections.Generic;
using System.Linq;

namespace Biblioteca.Models
{
    public class UsuarioService
    {   
        //Função que retorna uma lista de todos os usuarios do banco de dados
        public List<Usuario> Listar()
        {
            using(BibliotecaContext bc = new BibliotecaContext())
            {
                //Acessa o banco de dados, recebe todos os valores de 'Usuarios' e transforma em Lista
                return bc.Usuarios.ToList();
            }
        }

        //Função que busca um usuario do banco de dados pelo id
        public Usuario Buscar(int id)
        {
            using(BibliotecaContext bc = new BibliotecaContext())
            {
                //Acessa o banco de dados, busca pelo id em 'Usuarios'
                return bc.Usuarios.Find(id);
            }
        }

        //Função que adiciona um usuario no banco de dados
        public void adicionarUsuario(Usuario usuario)
        {
            using(BibliotecaContext bc = new BibliotecaContext())
            {
                //Adiciona o usuario no banco de dados
                bc.Add(usuario); //Vai para a tabela Usuarios automaticamente pelo tipo do objeto
                //Salva as alterações
                bc.SaveChanges();
            }
        }

        //Função que edita as informações de um usuario no banco de dados
        public void editarUsuario(Usuario usuario)
        {
            using(BibliotecaContext bc = new BibliotecaContext())
            {
                //Recebe o perfil antigo do usuario
                Usuario usuarioAntigo = bc.Usuarios.Find(usuario.Id);

                //Atualiza as informações no banco de dados
                usuarioAntigo.Login = usuario.Login;
                usuarioAntigo.Senha = usuario.Senha;
                usuarioAntigo.Nome = usuario.Nome;
                usuarioAntigo.Tipo = usuario.Tipo;

                //Salva as alterações
                bc.SaveChanges();
            }
        }

        //Função que exclui um usuário no banco de dados
        public void excluirUsuario(int id)
        {
            using(BibliotecaContext bc = new BibliotecaContext())
            {
                bc.Usuarios.Remove(bc.Usuarios.Find(id));
                bc.SaveChanges();
            }
        }
    }
}