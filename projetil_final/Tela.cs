using System;
using tabuleiro;
using xadrex;

namespace projetil_final
{
    class Tela
    {
        public static void printtab(Tabuleiro tab)
        {
            for(int i = 0; tab.linhas > i; i++)
            {
                Console.Write(8 - i + " ");
                for(int j = 0; tab.colunas > j; j++)
                {
                    if(tab.peca(i,j) == null)
                    {
                        Console.Write("- ");
                        continue;
                    }
                    imprimirPeca(tab.peca(i, j));
                    Console.Write(" ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }
        public static PosicaoXadrex lerPosicaoXadrex() {
            string s = Console.ReadLine();
            char coluna = s[0];
            int linha = int.Parse(s[1] + "");
            return new PosicaoXadrex(coluna, linha);
        }
        public static void imprimirPeca(Peca peca) {
            if(peca.cor == Cor.white) {
                Console.Write(peca);
            }
            else {
                ConsoleColor aux = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write(peca);
                Console.ForegroundColor = aux;
            }
        }
    }
}
