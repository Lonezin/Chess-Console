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
        public void IcrementarQteMovimentos()
        {
            QteMovimentos ++;
        }
    }
}