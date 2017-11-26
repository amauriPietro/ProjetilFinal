using System;
using tabuleiro;
using xadrex;

namespace projetil_final
{
    class Program
    {
        static void Main(string[] args)
        {
            Tabuleiro tab = new Tabuleiro(8, 8);

            tab.colocarPeca(new torre(tab, Cor.black), new Posicao(0, 0));
            tab.colocarPeca(new torre(tab, Cor.black), new Posicao(1, 3));
            tab.colocarPeca(new rei(tab, Cor.black), new Posicao(2, 4));
            Tela.printtab(tab);
            Console.ReadLine();
        }
    }
}
