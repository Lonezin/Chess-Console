using System.Drawing;
using Chess_Console_Tabuleiroz;

namespace Chess_Console.Xadrez
{
    public class Torre : Peca
    {
        public Torre (Tabuleiro tab, Cor cor) : base (tab, cor)
        {
        }
        public override string ToString()
        {
            return "T";
        }
    }
}