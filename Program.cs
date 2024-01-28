using Chess_Console_Tabuleiroz;

namespace Chess_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Tabuleiro tab = new Tabuleiro(8,8);
            Tela.ImprimirTela(tab);
        }
    }
}