using Chess_Console.Tabuleiroz;
using Chess_Console.Xadrez;
using Chess_Console_Tabuleiroz;

namespace Chess_Console
{
    class Program
    {
        static void Main(string[] args)
        {
           PosicaoXadrez pos = new PosicaoXadrez('a', 1);
           System.Console.WriteLine(pos);
        }
    }
}