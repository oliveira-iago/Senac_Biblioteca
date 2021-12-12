using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Biblioteca.Models
{
    //A BibliotecaContext é utilizada para fazer conexão com o banco de dados
    public class BibliotecaContext : DbContext
    {
        //Configura e referencia o banco de dados
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {                   
            optionsBuilder.UseMySql("Server=localhost;DataBase=Biblioteca;Uid=root;");
        }
        public DbSet<Livro> Livros {get; set;}
        public DbSet<Emprestimo> Emprestimos {get; set;}
        public DbSet<Usuario> Usuarios {get; set;}
    }
}
