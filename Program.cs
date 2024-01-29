using Chess_Console.Tabuleiroz;
using Chess_Console.Xadrez;
using Chess_Console_Tabuleiroz;

namespace Chess_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                PartidaDeXadrez partida = new PartidaDeXadrez();
                while (!partida.Terminada)
                {
                    Console.Clear();
                    Tela.ImprimirTela(partida.Tab);

                    Console.Write("Origem: ");
                    Posicao origem = Tela.LerPosicaoXadrez().ToPosicao();
                    Console.Write("Destino: ");
                    Posicao destino = Tela.LerPosicaoXadrez().ToPosicao();
                    Tela.ImprimirTela(partida.Tab);
                    partida.ExecutaMovimento(origem, destino);
                }
                
                
            }
            catch (TabuleiroException e)
            {
                System.Console.WriteLine(e.Message);
            }
        }
    }
}