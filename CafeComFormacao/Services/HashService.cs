using System.Security.Cryptography;
using CafeComFormacao.Interfaces.Util;

namespace CafeComFormacao.Services
{
    public class HashService : IHashService
    {
        private static RandomNumberGenerator rng = RandomNumberGenerator.Create();

        private static string CriarSal(int tamanho)
        {
            byte[] buff = new byte[tamanho];

            rng.GetBytes(buff);

            return Convert.ToBase64String(buff);
        }

        public string GerarHashSHA256(string credencial, string sal = "")
        {
            SHA256Managed sha256HashString = new();

            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(credencial);

            byte[] hash = sha256HashString.ComputeHash(bytes);

            string hashBase64 = Convert.ToBase64String(hash);

            return !string.IsNullOrEmpty(sal) ? sal + hashBase64 : hashBase64; ;
        }

        public string GerarCredenciaisSeguras(string senha)
        {
            string salSenha = CriarSal(16);

            string senhaSegura = GerarHashSHA256(senha, salSenha);

            return senhaSegura;
        }
    }
}
