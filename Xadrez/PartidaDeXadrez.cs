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
        public bool Xeque { get; private set; }
        public PartidaDeXadrez ()
        {
            Tab = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branca;
            Terminada = false;
            Xeque = false;
            Pecas = new HashSet<Peca>();
            Capturadas = new HashSet<Peca>();
            ColocarPecas();
        }
        public Peca ExecutaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = Tab.RetirarPeca(origem);
            p.IcrementarQteMovimentos();
            Peca pecaCapturada = Tab.RetirarPeca(destino);
            Tab.ColocarPeca(p, destino);
            if (pecaCapturada != null)
            {
                Capturadas.Add(pecaCapturada);
            }
            return pecaCapturada;
        }

        public void DesfazMovimento (Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            Peca p = Tab.RetirarPeca(destino);
            p.DescrementarQteMovimentos();
            if (pecaCapturada != null)
            {
                Tab.ColocarPeca(pecaCapturada, destino);
                Capturadas.Remove(pecaCapturada);
            }
            Tab.ColocarPeca(p, origem);
        }
        public void RealizaJogada(Posicao origem, Posicao destino)
        {
            Peca pecaCapturada = ExecutaMovimento(origem, destino);
            if (EstaEmXeque(JogadorAtual))
            {
                DesfazMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException("Voce nao pode se colocar em Xeque");
            }
            if (EstaEmXeque(Adversaria(JogadorAtual)))
            {
                Xeque = true;
            }
            else
            {
                Xeque = false;
            }
            if (TesteXequeMate(Adversaria(JogadorAtual)))
            {
                Terminada = true;
            }
            else{
                Turno++;
                MudaJogador();
            }
        }
        public void ValidarPosicaoDeDestino (Posicao origem, Posicao destino)
        {
            if (!Tab.Peca(origem).MovimentoPossivel(destino))
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
        private Cor Adversaria (Cor cor)
        {
            if (cor == Cor.Branca)
            {
                return Cor.Preta;
            }
            else
            {
                return Cor.Branca;
            }
        }
        private Peca Rei(Cor cor)
        {
            foreach (Peca x in PecasEmJogo(cor))
            {
                if(x is Rei)
                {
                    return x;
                }
            }
            return null;
        }

        public bool EstaEmXeque(Cor cor)
        {
            Peca R = Rei(cor);
        
            foreach (Peca x in PecasEmJogo(Adversaria(cor)))
            {
                bool [,] mat = x.MovimentosPossiveis();
                if (mat[R.Posicao.Linha, R.Posicao.Coluna])
                {
                    return true;
                }
            }
            return false;
        }
        public bool TesteXequeMate(Cor cor)
        {
            if(!EstaEmXeque(cor))
            {
                return false;
            }
            foreach (Peca x in PecasEmJogo(cor))
            {
                bool [,] mat = x.MovimentosPossiveis();
                for (int i = 0; i < Tab.Linha; i++)
                {
                    for (int j = 0; j < Tab.Coluna; j++)
                    {
                        if(mat[i,j])
                        {
                            Posicao origem = x.Posicao;
                            Posicao destino = new Posicao(i,j);
                            Peca pecaCapturada = ExecutaMovimento(origem, destino);
                            bool testaXeque = EstaEmXeque(cor);
                            DesfazMovimento(origem, destino, pecaCapturada);
                            if (!testaXeque)
                            {
                                return false;
                            }
                        }    
                    }
                } 
            }
            return true;
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
            ColocaNovaPeca('h', 7, new Torre(Tab, Cor.Branca));
            ColocaNovaPeca('e', 2, new Torre(Tab, Cor.Branca));
            ColocaNovaPeca('e', 1, new Torre(Tab, Cor.Branca));
            ColocaNovaPeca('d', 2, new Peao(Tab, Cor.Branca));
            ColocaNovaPeca('d', 1, new Rei(Tab, Cor.Branca));


            ColocaNovaPeca('a', 8, new Rei(Tab, Cor.Preta));
            ColocaNovaPeca('b', 8, new Torre(Tab, Cor.Preta));
            ColocaNovaPeca('c', 5, new Torre(Tab, Cor.Preta));
            ColocaNovaPeca('e', 5, new Torre(Tab, Cor.Preta));
            ColocaNovaPeca('d', 4, new Torre(Tab, Cor.Preta));
        }
    }
}