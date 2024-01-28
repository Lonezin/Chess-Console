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
                        Console.Write (" -");
                    }
                    Console.Write($"{tab.Peca(i,j)} ");           
                }
                System.Console.WriteLine();
            }
            System.Console.WriteLine("  A  B  C  D  E  F  G  H");
        }
    }
}