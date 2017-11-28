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
                Tabuleiro tab = new Tabuleiro(8, 8);

                tab.colocarPeca(new torre(tab, Cor.black), new Posicao(0, 0));
                tab.colocarPeca(new torre(tab, Cor.black), new Posicao(1, 3));
                tab.colocarPeca(new rei(tab, Cor.black), new Posicao(0, 2));
                tab.colocarPeca(new torre(tab, Cor.white), new Posicao(7, 7));
                tab.colocarPeca(new rei(tab, Cor.white), new Posicao(2, 5));
                Tela.printtab(tab);

            }
            catch(TabuleiroException e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadLine(); 
        }
    }
}
