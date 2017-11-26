using System;
using tabuleiro;

namespace projetil_final
{
    class Program
    {
        static void Main(string[] args)
        {
            Tabuleiro tab = new Tabuleiro(8, 8);

            Tela.printtab(tab);
            Console.ReadLine();
        }
    }
}
