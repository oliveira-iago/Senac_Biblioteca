using System.Security.Cryptography;
using System.Text;

namespace Biblioteca.Controllers
{
    public class Criptografo
    {
        public static string Criptografar(string texto)
        {
            //Modelo de encriptação MD5
            MD5 MD5Hasher = MD5.Create();

            //Consome o MD5 Hasher para "criptografar" o texto
            byte[] By = Encoding.Default.GetBytes(texto);
            byte[] bytesCriptografado = MD5Hasher.ComputeHash(By);

            StringBuilder SB = new StringBuilder();

            //Loop para transformar o texto criptografado em string
            foreach(byte b in bytesCriptografado)
            {
                string DebugB = b.ToString("x2");
                SB.Append(DebugB);
            }

            //Retorna o texto criptografado
            return SB.ToString();
        }
    }
}