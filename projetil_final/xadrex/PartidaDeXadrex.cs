using System;
using tabuleiro;
using xadrex;

namespace xadrex {
    class PartidaDeXadrex {
        public Tabuleiro tab { get; private set; }
        public int turno { get; private set; }
        public Cor jogadorAtual { get; private set; }
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
        public void realizaJogada(Posicao origem, Posicao destino)
        {
            executaMovimento(origem, destino);
            turno++;
            mudaJogador();
        }
        public void validarPosicaoDeOrigem(Posicao pos)
        {
            if(tab.peca(pos) == null)
            {
                throw new TabuleiroException("Não existe peça na posicao na posicao selecionada!");
            }
            if(jogadorAtual != tab.peca(pos).cor)
            {
                throw new TabuleiroException("A peça selecionada não é sua!");
            }
            if (!tab.peca(pos).existeMovimentosPossiveis())
            {
                throw new TabuleiroException("Não há movimentos para a peça selecionada!");
            }
        }
        public void validarPosicaoDeDestino(Posicao origem, Posicao destino)
        {
            if (!tab.peca(origem).podeMoverPara(destino))
            {
                throw new TabuleiroException("Posicao de destino invalida!");
            }
        }
        private void mudaJogador()
        {
            if(jogadorAtual == Cor.white)
            {
                jogadorAtual = Cor.black;
            }
            else
            {
                jogadorAtual = Cor.white;
            }
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
