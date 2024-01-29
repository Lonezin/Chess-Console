using Chess_Console.Xadrez;
using Chess_Console_Tabuleiroz;

namespace Chess_Console
{
    public class Tela
    {
        public static void ImprimirTela(Tabuleiro tab)
        {
            for (int i = 0; i < tab.Linha; i++)
            { 
                Console.Write(8 - i);
                for (int j = 0; j < tab.Coluna; j++)
                {
                    if (tab.Peca(i,j) == null)
                    {
                        Console.Write (" - ");
                    }
                    else
                    {
                        ImprimirPeca(tab.Peca(i,j));
                        Console.Write(" ");
                    }           
                }
                System.Console.WriteLine();
            }
            System.Console.WriteLine("  A  B  C  D  E  F  G  H");
        }
        public static PosicaoXadrez LerPosicaoXadrez()
        {
            string s = Console.ReadLine();
            char coluna = s[0];
            int  linha = int.Parse(s[1] + "");
            return new PosicaoXadrez(coluna, linha);
        }
        public static void ImprimirPeca(Peca peca)
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
    }
}