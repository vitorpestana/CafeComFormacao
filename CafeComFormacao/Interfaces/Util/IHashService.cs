namespace CafeComFormacao.Interfaces.Util
{
    public interface IHashService
    {
        string GerarHashSHA256(string credencial, string sal);
        (string, string) GerarCredenciaisSeguras(string usuario, string senha);
    }
}
