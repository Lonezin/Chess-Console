using System.Drawing;
using Chess_Console_Tabuleiroz;

namespace Chess_Console.Xadrez
{
    public class Torre : Peca
    {
        public Torre (Tabuleiro tab, Cor cor) : base (tab, cor)
        {
        }
        private bool PodeMover(Posicao pos)
        {
            Peca p = Tab.Peca(pos);
            return p == null || p.Cor != Cor;
        }
        public override bool[,] MovimentosPossiveis()
        {
            bool [,] mat = new bool[Tab.Linha, Tab.Coluna];
            Posicao pos = new Posicao(0, 0);

            //acima
            pos.DefinirValores(pos.Linha - 1, pos.Coluna);
            while(Tab.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
                if(Tab.Peca(pos) != null && Tab.Peca(pos).Cor != Cor)
                {
                    break;
                }
                pos.Linha = pos.Linha - 1;
            }
            //abaixo
            pos.DefinirValores(pos.Linha + 1, pos.Coluna);
            while(Tab.PosicaoValida(pos) && PodeMover(pos))
            {
                if (Tab.Peca(pos) != null && Tab.Peca(pos).Cor != Cor)
                {
                    break;
                }
                pos.Linha = pos.Linha + 1;
            }    
            //direita
            pos.DefinirValores(pos.Linha, pos.Coluna + 1);
            while (Tab.PosicaoValida(pos) && PodeMover(pos))
            {
                if (Tab.Peca(pos) != null && Tab.Peca(pos).Cor != Cor)
                {
                    break;
                }
            }
            return mat;
        }
        public override string ToString()
        {
            return "T";
        }
    }
}