namespace Chess_Console_Tabuleiroz

{
    public abstract class Peca
    {
        public Posicao Posicao { get; set; }
        public Cor Cor { get; protected set; }
        public int QteMovimentos  { get; protected set; }   
        public Tabuleiro Tab { get; protected set; } 

        public Peca (Tabuleiro tab, Cor cor)
        {
            Posicao = null;
            Tab = tab;
            Cor = cor;
            QteMovimentos = 0;
        }
        public abstract bool [,] MovimentosPossiveis();
        public void DescrementarQteMovimentos()
        {
            QteMovimentos --;
        }
        public void IcrementarQteMovimentos()
        {
            QteMovimentos ++;
        }
        public bool ExisteMovimentoPossiveis()
        {
            bool [,] mat = MovimentosPossiveis();
            for (int i = 0; i < Tab.Linha; i++)
            {
                for (int j = 0; j < Tab.Coluna; j++)
                {
                    if (mat[i, j])
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public bool PodeMoverPara(Posicao pos)
        {
            return MovimentosPossiveis()[pos.Linha, pos.Coluna];
        }
    }
}