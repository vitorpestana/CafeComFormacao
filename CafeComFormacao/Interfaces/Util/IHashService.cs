namespace CafeComFormacao.Interfaces.Util
{
    public interface IHashService
    {
        string CriarSal(int tamanho);
        string GerarHashSHA256(string credencial, string sal = "");
        string GerarCredenciaisSeguras(string senha);
    }
}
