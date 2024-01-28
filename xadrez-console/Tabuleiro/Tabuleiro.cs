namespace Chess_Console_Tabuleiroz
{
    public class Tabuleiro
    {
        public int Linha { get; set; }
        public int Coluna { get; set; }
        public Peca[,] Pecas;

        public Tabuleiro (int linha, int coluna)
        {
            Linha = linha;
            Coluna = coluna;
            Pecas = new Peca[linha, coluna];
        }
        public Peca Peca(int linha, int coluna)
        {
            return Pecas[linha, coluna];
        }
    }
}