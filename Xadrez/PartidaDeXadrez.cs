using System.Drawing;
using Chess_Console.Tabuleiroz;
using Chess_Console_Tabuleiroz;

namespace Chess_Console.Xadrez
{
    public class PartidaDeXadrez
    {
        public Tabuleiro Tab { get; private set; }
        public int Turno { get; private set; }
        public Cor JogadorAtual { get; private set; } 
        public bool Terminada { get; private set; }
        private HashSet<Peca> Pecas;
        private HashSet<Peca> Capturadas;
        public PartidaDeXadrez ()
        {
            Tab = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branca;
            Terminada = false;
            Pecas = new HashSet<Peca>();
            Capturadas = new HashSet<Peca>();
            ColocarPecas();
        }
        public void ExecutaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = Tab.RetirarPeca(origem);
            p.IcrementarQteMovimentos();
            Peca pecaCapturada = Tab.RetirarPeca(destino);
            Tab.ColocarPeca(p, destino);
            if (pecaCapturada != null)
            {
                Capturadas.Add(pecaCapturada);
            }
        }
        public void RealizaJogada(Posicao origem, Posicao destino)
        {
            ExecutaMovimento(origem, destino);
            Turno++;
            MudaJogador();
        }
        public void ValidarPosicaoDeDestino (Posicao origem, Posicao destino)
        {
            if (!Tab.Peca(origem).PodeMoverPara(destino))
            {
                throw new TabuleiroException("Posicao de destino invalida");
            }
        }
        public void ValidarPosicaoDeOrigem(Posicao pos)
        {
            if (Tab.Peca(pos) == null)
            {
                throw new TabuleiroException("Nao existe peca na posicao escolhida");
            }
            if (JogadorAtual != Tab.Peca(pos).Cor)
            {
                throw new TabuleiroException("A peca escolhida nao e sua");
            }
            if (!Tab.Peca(pos).ExisteMovimentoPossiveis())
            {
                throw new TabuleiroException("Nao ha movimentos possiveis para a peca de origem");
            }
        }
        private void MudaJogador()
        {
            if(JogadorAtual == Cor.Branca)
            {
                JogadorAtual = Cor.Preta;
            }
            else
            {
                JogadorAtual = Cor.Branca;
            }
        }   
        
        public HashSet<Peca> PecasCapturadas(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in Capturadas)
            {
                if (x.Cor == cor)
                {
                    aux.Add(x);
                } 
            }
            return aux;
        }

        public HashSet<Peca> PecasEmJogo(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in Pecas)
            {
                if (x.Cor == cor)
                {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(PecasCapturadas(cor));
            return aux;
        }

        public void ColocaNovaPeca(char coluna, int linha, Peca peca)
        {
            Tab.ColocarPeca(peca, new PosicaoXadrez(coluna, linha).ToPosicao());
            Pecas.Add(peca);        
        }
        private void ColocarPecas()
        {
            ColocaNovaPeca('c', 1, new Torre(Tab, Cor.Branca));
            ColocaNovaPeca('c', 2, new Torre(Tab, Cor.Branca));
            ColocaNovaPeca('d', 2, new Torre(Tab, Cor.Branca));
            ColocaNovaPeca('e', 2, new Torre(Tab, Cor.Branca));
            ColocaNovaPeca('e', 1, new Torre(Tab, Cor.Branca));
            ColocaNovaPeca('d', 1, new Rei(Tab, Cor.Branca));


            ColocaNovaPeca('d', 8, new Rei(Tab, Cor.Preta));
            ColocaNovaPeca('c', 8, new Torre(Tab, Cor.Preta));
            ColocaNovaPeca('d', 7, new Torre(Tab, Cor.Preta));
            ColocaNovaPeca('e', 7, new Torre(Tab, Cor.Preta));
            ColocaNovaPeca('e', 8, new Torre(Tab, Cor.Preta));
            ColocaNovaPeca('c', 7, new Torre(Tab, Cor.Preta));
        }
    }
}