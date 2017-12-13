using System;
using tabuleiro;
using System.Collections.Generic;
using xadrex;

namespace projetil_final
{
    class Tela
    {
        public static void imprimirPartida(PartidaDeXadrex partida) {
            printtab(partida.tab);
            Console.WriteLine();
            imprimirPecasCapturadas(partida);
            Console.WriteLine("\nTurno: " + partida.turno);
            if (!partida.terminada)
            {
                Console.WriteLine("Waiting for " + partida.jogadorAtual + " play");
                if (partida.xeque)
                {
                    Console.WriteLine("XeQuE!");
                }
            }
            else
            {
                Console.WriteLine("XEQUE-MATE!");
                Console.WriteLine("Winner: " + partida.jogadorAtual);
            }
        }
        public static void imprimirPecasCapturadas(PartidaDeXadrex partida) {
            Console.WriteLine("Pecas Capturadas: ");
            Console.Write("brancas: ");
            imprimirConjunto(partida.pecasCapturadas(Cor.white));
            Console.Write("\npretas: ");
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Magenta;
            imprimirConjunto(partida.pecasCapturadas(Cor.black));
            Console.ForegroundColor = aux;
        }
        public static void imprimirConjunto(HashSet<Peca> conjunto) {
            Console.Write("[");
            foreach(Peca x in conjunto) {
                Console.Write(x + " ");
            }
            Console.Write("]");
        }
        public static void printtab(Tabuleiro tab)
        {
            for(int i = 0; tab.linhas > i; i++)
            {
                Console.Write(8 - i + " ");
                for(int j = 0; tab.colunas > j; j++)
                {
                    imprimirPeca(tab.peca(i, j));
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }
        public static void printtab(Tabuleiro tab, bool[,] posicoesPossiveis) {
            ConsoleColor fundoOriginal = Console.BackgroundColor;
            ConsoleColor fundoAlterado = ConsoleColor.DarkBlue;

            for (int i = 0; tab.linhas > i; i++) {
                Console.Write(8 - i + " ");
                for (int j = 0; tab.colunas > j; j++) {
                    if(posicoesPossiveis[i, j] == true) {
                        Console.BackgroundColor = fundoAlterado;
                    }
                    else {
                        Console.BackgroundColor = fundoOriginal;
                    }
                    imprimirPeca(tab.peca(i, j));
                    Console.BackgroundColor = fundoOriginal;
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
            Console.BackgroundColor = fundoOriginal;
        }
        public static PosicaoXadrex lerPosicaoXadrex() {
            string s = Console.ReadLine();
            char coluna = s[0];
            int linha = int.Parse(s[1] + "");
            return new PosicaoXadrex(coluna, linha);
        }
        public static void imprimirPeca(Peca peca) {

            if(peca == null) {
                Console.Write("- ");
            }
            else {
                if(peca.cor == Cor.white) {
                    Console.Write(peca);
                }
                else {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write(peca);
                    Console.ForegroundColor = aux;
                }
                Console.Write(" ");
            }
        }
    }
}
