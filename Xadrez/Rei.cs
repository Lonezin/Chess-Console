using Chess_Console_Tabuleiroz;

namespace Chess_Console.Xadrez
{
    public class Rei : Peca
    {
        public Rei (Tabuleiro tab, Cor cor) : base (tab, cor)
        {
        }
        public override string ToString()
        {
            return "R";
        }
    }
}