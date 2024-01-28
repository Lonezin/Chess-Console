using Chess_Console.Tabuleiroz;

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
        public Peca Peca(Posicao pos)
        {
            return Pecas[pos.Linha, pos.Coluna];
        }
        public bool ExistePeca(Posicao pos)
        {
            ValidarPosicao(pos);
            return Peca(pos) != null;
        }
        public void ColocarPeca(Peca p, Posicao pos)
        {
            Pecas[pos.Linha, pos.Coluna] = p;
            p.Posicao = pos;
        }
        public bool PosicaoValida (Posicao pos)
        {
            if (pos.Linha < 0 || pos.Linha >= Linha || pos.Coluna < 0 || pos.Coluna >= Coluna)
            {
                return false;
            }
            return true;
        }
        public void ValidarPosicao(Posicao pos)
        {
            if(!PosicaoValida(pos))
            {
                throw new TabuleiroException ("Posição inválida"); 
            }

        }
    }
}