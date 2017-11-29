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
                    Console.Clear();
                    Tela.printtab(partida.tab);
                    Console.Write("Origem: ");
                    Posicao origem = Tela.lerPosicaoXadrex().toPosicao();
                    bool[,] posicoesPossiveis = partida.tab.peca(origem).movimentosPossiveis();
                    Console.Clear();
                    Tela.printtab(partida.tab, posicoesPossiveis);
                    Console.Write("Destino: ");
                    Posicao destino = Tela.lerPosicaoXadrex().toPosicao();
                    partida.executaMovimento(origem, destino);
                }

            }
            catch(TabuleiroException e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadLine(); 
        }
    }
}
