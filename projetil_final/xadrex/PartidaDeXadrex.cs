using System;
using tabuleiro;

namespace xadrex {
    class PartidaDeXadrex {
        public Tabuleiro tab { get; private set; }
        private int turno;
        private Cor jogadorAtual;
        public bool terminada { get; private set; }

        public PartidaDeXadrex() {
            tab = new Tabuleiro(8, 8);
            turno = 1;
            jogadorAtual = Cor.white;
            colocarPecas();
        }
        public void executaMovimento(Posicao origem, Posicao destino) {
            Peca p = tab.retirarPeca(origem);
            Peca pecaCapturada = tab.retirarPeca(destino);
            terminada = false;
            tab.colocarPeca(p, destino);
        }
        private void colocarPecas() {
            tab.colocarPeca(new torre(tab, Cor.white), new PosicaoXadrex('c', 1).toPosicao());
            tab.colocarPeca(new torre(tab, Cor.white), new PosicaoXadrex('c', 2).toPosicao());
            tab.colocarPeca(new torre(tab, Cor.white), new PosicaoXadrex('d', 2).toPosicao());
            tab.colocarPeca(new torre(tab, Cor.white), new PosicaoXadrex('e', 2).toPosicao());
            tab.colocarPeca(new torre(tab, Cor.white), new PosicaoXadrex('e', 1).toPosicao());
            tab.colocarPeca(new rei(tab, Cor.white), new PosicaoXadrex('d', 1).toPosicao());
            tab.colocarPeca(new torre(tab, Cor.black), new PosicaoXadrex('c', 7).toPosicao());
            tab.colocarPeca(new torre(tab, Cor.black), new PosicaoXadrex('c', 8).toPosicao());
            tab.colocarPeca(new torre(tab, Cor.black), new PosicaoXadrex('d', 7).toPosicao());
            tab.colocarPeca(new torre(tab, Cor.black), new PosicaoXadrex('e', 8).toPosicao());
            tab.colocarPeca(new torre(tab, Cor.black), new PosicaoXadrex('e', 7).toPosicao());
            tab.colocarPeca(new rei(tab, Cor.black), new PosicaoXadrex('d', 8).toPosicao());

        }
    }
}
