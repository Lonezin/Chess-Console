using Chess_Console.Xadrez;
using Chess_Console_Tabuleiroz;

namespace Chess_Console
{
    public class Tela
    {
        public static void ImprimirPartida(PartidaDeXadrez partida)
        {
            ImprimirTela(partida.Tab);
            System.Console.WriteLine();
            ImprimiePecaCapturada(partida);
            System.Console.WriteLine();
            Console.Write($"Turno: {partida.Turno} ");
            Console.WriteLine($"Aguardando jogada: {partida.JogadorAtual}");
            if (partida.Xeque)
            {
                System.Console.WriteLine("XEQUE!");
            }
        } 
        public static void ImprimiePecaCapturada(PartidaDeXadrez partida)
        {
            System.Console.WriteLine("Pecas capturadas: ");
            Console.Write("Brancas: ");
            ImprimirConjunto(partida.PecasCapturadas(Cor.Branca));
            Console.Write("Pretas: ");
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            ImprimirConjunto(partida.PecasCapturadas(Cor.Preta));
            Console.ForegroundColor = aux;
            System.Console.WriteLine();
        }
        public static void ImprimirConjunto(HashSet<Peca> conjunto)
        {
            Console.Write("[ ");
            foreach (Peca x in conjunto)
            {
                Console.Write($"{x} ");
            }
            Console.Write("]");
        }
        public static void ImprimirTela(Tabuleiro tab)
        {
            for (int i = 0; i < tab.Linha; i++)
            { 
                Console.Write(8 - i);
                for (int j = 0; j < tab.Coluna; j++)
                {
                    ImprimirPeca(tab.Peca(i,j));
                    Console.Write(" ");          
                }
                System.Console.WriteLine();
            }
            System.Console.WriteLine("  A  B  C  D  E  F  G  H");
        }
        public static void ImprimirTela(Tabuleiro tab, bool[,] posicoesPossiveis)
        {
            ConsoleColor fundoOriginal = Console.BackgroundColor;
            ConsoleColor fundoAlterado = ConsoleColor.DarkGray;

            for (int i = 0; i < tab.Linha; i++)
            {
                Console.Write(8 - i);
                for (int j = 0; j < tab.Coluna; j++)
                {
                    if(posicoesPossiveis[i,j] == true)
                    {
                        Console.BackgroundColor = fundoAlterado;
                    }
                    else
                    {
                        Console.BackgroundColor = fundoOriginal;    
                    }
                    ImprimirPeca(tab.Peca(i,j));
                    Console.Write(" ");
                }
                System.Console.WriteLine();
            }
            System.Console.WriteLine("  A  B  C  D  E  F  G  H");
            Console.BackgroundColor = fundoOriginal;
        }
        public static PosicaoXadrez LerPosicaoXadrez()
        {
            string s = Console.ReadLine();
            char coluna = s[0];
            int  linha = int.Parse(s[1].ToString());
            return new PosicaoXadrez(coluna, linha);
        }
        public static void ImprimirPeca(Peca peca)
        {
            if (peca == null)
            {
                Console.Write (" -");
            }
            else 
            {    
                if (peca.Cor == Cor.Branca)
                {
                    Console.Write($" {peca}");
                }
                else
                {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write($" {peca}");
                    Console.ForegroundColor = aux;
                }
            }
            Console.Write("");
        }
    }
}