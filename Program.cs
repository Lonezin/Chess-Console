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
                    try{
                    Console.Clear();
                    Tela.ImprimirPartida(partida);
                    System.Console.WriteLine();
                    Console.WriteLine($"Turno: {partida.Turno}");
                    Console.WriteLine($"Aguardando jogada: {partida.JogadorAtual}");
                    Console.Write("Origem: ");
                    Posicao origem = Tela.LerPosicaoXadrez().ToPosicao();
                    partida.ValidarPosicaoDeOrigem(origem);
                    bool [,] posicoesPossiveis = partida.Tab.Peca(origem).MovimentosPossiveis();
                    Console.Clear();
                    Tela.ImprimirTela(partida.Tab, posicoesPossiveis);
                    Console.Write("Destino: ");
                    Posicao destino = Tela.LerPosicaoXadrez().ToPosicao();
                    partida.ValidarPosicaoDeDestino(origem, destino);
                    Tela.ImprimirTela(partida.Tab);
                    partida.RealizaJogada(origem, destino);
                    }
                    catch (TabuleiroException e)
                    {
                        System.Console.WriteLine(e.Message);
                        Console.ReadLine();
                    }
                }    
            }
            catch (TabuleiroException e)
            {
                System.Console.WriteLine(e.Message);
            }
        }
    }
}