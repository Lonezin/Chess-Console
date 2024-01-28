using Chess_Console_Tabuleiroz;

namespace Chess_Console.Xadrez
{
    public class PosicaoXadrez
    {
        public char Coluna  { get; set; }
        public int Linha { get; set; }

        public PosicaoXadrez (char coluna, int linha)
        {
           Coluna = coluna;
           Linha = linha; 
        }
        public Posicao ToPosicao(Posicao pos)
        {
            return new Posicao(8 - Linha, Coluna - 'a');
        }
        public override string ToString()
        {
            return $"{Coluna}{Linha}";
        }
    }
}