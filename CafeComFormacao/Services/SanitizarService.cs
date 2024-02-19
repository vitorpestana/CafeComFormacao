using CafeComFormacao.Interfaces.Util;
using System.Text.RegularExpressions;

namespace CafeComFormacao.Services
{
    public class SanitizarService : ISanitizarService
    {
        public string RetirarCaracteresPossivelmenteMaliciosos(string inputPossivelmenteMalicioso)
        {
            return Regex.Replace(inputPossivelmenteMalicioso, "[\"\\'\\=\\*\\[\\]\\(\\)\\{\\}\\:\\;\\/\\,\\!\\?\\|\\&\\#]", String.Empty);
        }
    }
}
