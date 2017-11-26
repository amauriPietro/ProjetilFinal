using System;
using tabuleiro;

namespace projetil_final
{
    class Tela
    {
        public static void printtab(Tabuleiro tab)
        {
            for(int i = 0; tab.linhas > i; i++)
            {
                for(int j = 0; tab.colunas > j; j++)
                {
                    if(tab.peca(i,j) == null)
                    {
                        Console.Write("- ");
                        continue;
                    }
                    Console.Write(tab.peca(i, j) + " ");
                }
                Console.WriteLine();
            }
        }

    }
}
