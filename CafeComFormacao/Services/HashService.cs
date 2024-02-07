using System.Security.Cryptography;
using CafeComFormacao.Interfaces.Util;

namespace CafeComFormacao.Services
{
    public class HashService : IHashService
    {
        private static RandomNumberGenerator rng = RandomNumberGenerator.Create();

        private static string CriarSal(int size)
        {
            byte[] buff = new byte[size];

            rng.GetBytes(buff);

            return Convert.ToBase64String(buff);
        }

        public string GerarHashSHA256(string credencial, string sal)
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(credencial + sal);

            SHA256Managed sha256HashString = new();

            byte[] hash = sha256HashString.ComputeHash(bytes);

            return Convert.ToBase64String(hash);
        }

        public (string, string) GerarCredenciaisSeguras(string usuario, string senha)
        {
            string salSenha = CriarSal(11);

            string senhaSegura = GerarHashSHA256(senha, salSenha);

            string salUsuario = CriarSal(11);

            string usuarioSeguro = GerarHashSHA256(usuario, salUsuario);

            return (usuarioSeguro, senhaSegura);
        }
    }
}
