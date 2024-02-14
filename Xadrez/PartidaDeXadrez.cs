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
            //#Jogada Especial roque pequeno
            if (p is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemDaTorre = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao destinoDaTorre = new Posicao(origem.Linha, origem.Coluna + 1);
                Peca T = Tab.RetirarPeca(origemDaTorre);
                T.IcrementarQteMovimentos();
                Tab.ColocarPeca(T, destinoDaTorre);
            }
            //#Jogada Especial roque grande
            if (p is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemDaTorre = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao destinoDaTorre = new Posicao(origem.Linha, origem.Coluna - 1);
                Peca T = Tab.RetirarPeca(origemDaTorre);
                T.IcrementarQteMovimentos();
                Tab.ColocarPeca(T, destinoDaTorre);
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

            //#Jogada Especial roque pequeno
            if (p is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemDaTorre = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao destinoDaTorre = new Posicao(origem.Linha, origem.Coluna + 1);
                Peca T = Tab.RetirarPeca(destinoDaTorre);
                T.DescrementarQteMovimentos();
                Tab.ColocarPeca(T, origemDaTorre);
            }
            //#Jogada Especial roque grande
            if (p is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemDaTorre = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao destinoDaTorre = new Posicao(origem.Linha, origem.Coluna - 1);
                Peca T = Tab.RetirarPeca(destinoDaTorre);
                T.DescrementarQteMovimentos();
                Tab.ColocarPeca(T, origemDaTorre);
            }
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
            ColocaNovaPeca('a', 1, new Torre(Tab, Cor.Branca));
            ColocaNovaPeca('b', 1, new Cavalo(Tab, Cor.Branca));
            ColocaNovaPeca('c', 1, new Bispo(Tab, Cor.Branca));
            ColocaNovaPeca('e', 1, new Torre(Tab, Cor.Branca));
            ColocaNovaPeca('e', 1, new Rei(Tab, Cor.Branca, this));
            ColocaNovaPeca('d', 1, new Dama(Tab, Cor.Branca));
            ColocaNovaPeca('f', 1, new Bispo(Tab, Cor.Branca));
            ColocaNovaPeca('g', 1, new Cavalo(Tab, Cor.Branca));
            ColocaNovaPeca('h', 1, new Torre(Tab, Cor.Branca));
            ColocaNovaPeca('a', 2, new Peao(Tab, Cor.Branca));
            ColocaNovaPeca('b', 2, new Peao(Tab, Cor.Branca));
            ColocaNovaPeca('c', 2, new Peao(Tab, Cor.Branca));
            ColocaNovaPeca('d', 2, new Peao(Tab, Cor.Branca));
            ColocaNovaPeca('e', 2, new Peao(Tab, Cor.Branca));
            ColocaNovaPeca('f', 2, new Peao(Tab, Cor.Branca));
            ColocaNovaPeca('g', 2, new Peao(Tab, Cor.Branca));
            ColocaNovaPeca('h', 2, new Peao(Tab, Cor.Branca));

            ColocaNovaPeca('a', 8, new Torre(Tab, Cor.Preta));
            ColocaNovaPeca('b', 8, new Cavalo(Tab, Cor.Preta));
            ColocaNovaPeca('c', 8, new Bispo(Tab, Cor.Preta));
            ColocaNovaPeca('e', 8, new Torre(Tab, Cor.Preta));
            ColocaNovaPeca('e', 8, new Rei(Tab, Cor.Preta, this));
            ColocaNovaPeca('d', 8, new Dama(Tab, Cor.Preta));
            ColocaNovaPeca('f', 8, new Bispo(Tab, Cor.Preta));
            ColocaNovaPeca('g', 8, new Cavalo(Tab, Cor.Preta));
            ColocaNovaPeca('h', 8, new Torre(Tab, Cor.Preta));
            ColocaNovaPeca('a', 7, new Peao(Tab, Cor.Preta));
            ColocaNovaPeca('b', 7, new Peao(Tab, Cor.Preta));
            ColocaNovaPeca('c', 7, new Peao(Tab, Cor.Preta));
            ColocaNovaPeca('d', 7, new Peao(Tab, Cor.Preta));
            ColocaNovaPeca('e', 7, new Peao(Tab, Cor.Preta));
            ColocaNovaPeca('f', 7, new Peao(Tab, Cor.Preta));
            ColocaNovaPeca('g', 7, new Peao(Tab, Cor.Preta));
            ColocaNovaPeca('h', 7, new Peao(Tab, Cor.Preta));

            
        }
    }
}