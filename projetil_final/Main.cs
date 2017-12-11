using System;
using tabuleiro;
using xadrex;

namespace projetil_final
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                PartidaDeXadrex partida = new PartidaDeXadrex();
                while (!partida.terminada) {
                    try
                    {
                        Console.Clear();
                        Tela.printtab(partida.tab);
                        Console.WriteLine("\nTurno: " + partida.turno);
                        Console.WriteLine("Waiting for " + partida.jogadorAtual + " play");
                        Console.Write("Origem: ");
                        Posicao origem = Tela.lerPosicaoXadrex().toPosicao();
                        partida.validarPosicaoDeOrigem(origem);
                        bool[,] posicoesPossiveis = partida.tab.peca(origem).movimentosPossiveis();
                        Console.Clear();
                        Tela.printtab(partida.tab, posicoesPossiveis);
                        Console.Write("Destino: ");
                        Posicao destino = Tela.lerPosicaoXadrex().toPosicao();
                        partida.validarPosicaoDeDestino(origem, destino);
                        partida.realizaJogada(origem, destino);
                    }
                    catch(TabuleiroException e)
                    {
                        Console.WriteLine(e.Message);
                        Console.ReadLine();
                    }
                }

            }
            catch(TabuleiroException e)
            {
                Console.WriteLine(e.Message);
                Console.ReadLine();
            }
            Console.ReadLine(); 
        }
    }
}
