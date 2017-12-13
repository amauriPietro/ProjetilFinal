using System.Collections.Generic;
using tabuleiro;
using xadrex;

namespace xadrex {
    class PartidaDeXadrex {
        public Tabuleiro tab { get; private set; }
        public int turno { get; private set; }
        public Cor jogadorAtual { get; private set; }
        public bool terminada { get; private set; }
        private HashSet<Peca> pecas;
        private HashSet<Peca> capturadas;
        public bool  xeque { get; private set; }

        public PartidaDeXadrex() {
            tab = new Tabuleiro(8, 8);
            turno = 1;
            jogadorAtual = Cor.white;
            terminada = false;
            xeque = false;
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();
            colocarPecas();
        }
        public Peca executaMovimento(Posicao origem, Posicao destino) {
            Peca p = tab.retirarPeca(origem);
            p.incrementarQteMovimentos();
            Peca pecaCapturada = tab.retirarPeca(destino);
            tab.colocarPeca(p, destino);
            if (pecaCapturada != null)
            {
                capturadas.Add(pecaCapturada);
            }
            //#special gameplays liro roque
            if(p is rei && destino.coluna == origem.coluna + 2) {
                Posicao origemT = new Posicao(origem.linha, origem.coluna + 3);
                Posicao destinoT = new Posicao(origem.linha, origem.coluna + 1);
                Peca T = tab.retirarPeca(origemT);
                T.incrementarQteMovimentos();
                tab.colocarPeca(T, destinoT);
            }
            //#special gameplays big roque
            if (p is rei && destino.coluna == origem.coluna - 2) {
                Posicao origemT = new Posicao(origem.linha, origem.coluna - 4);
                Posicao destinoT = new Posicao(origem.linha, origem.coluna - 1);
                Peca T = tab.retirarPeca(origemT);
                T.incrementarQteMovimentos();
                tab.colocarPeca(T, destinoT);
            }
            return pecaCapturada;
        }
        public void realizaJogada(Posicao origem, Posicao destino)
        {
            Peca pecaCapturada = executaMovimento(origem, destino);
            if (estaEmXeque(jogadorAtual)) {
                desfazMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException("Voce nao pode se colocar em xeque!");
            }
            if (estaEmXeque(adversaria(jogadorAtual))) {
                xeque = true;
            }
            else {
                xeque = false;
            }
            if (testeXequeMate(adversaria(jogadorAtual)))
            {
                terminada = true;
            }
            else
            {
                turno++;
                mudaJogador();
            }
        }
        public void desfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada) {
            Peca p = tab.retirarPeca(destino);
            p.decrementarQteMovimentos();
            if(pecaCapturada != null) {
                tab.colocarPeca(pecaCapturada, destino);
                capturadas.Remove(pecaCapturada);
            }
            tab.colocarPeca(p, origem);
            //#special gameplays liro roque
            if (p is rei && destino.coluna == origem.coluna + 2) {
                Posicao origemT = new Posicao(origem.linha, origem.coluna + 3);
                Posicao destinoT = new Posicao(origem.linha, origem.coluna + 1);
                Peca T = tab.retirarPeca(destinoT);
                T.decrementarQteMovimentos();
                tab.colocarPeca(T, origemT);
            }
            //#special gameplays big roque
            if (p is rei && destino.coluna == origem.coluna - 2) {
                Posicao origemT = new Posicao(origem.linha, origem.coluna - 4);
                Posicao destinoT = new Posicao(origem.linha, origem.coluna - 1);
                Peca T = tab.retirarPeca(destinoT);
                T.decrementarQteMovimentos();
                tab.colocarPeca(T, origemT);
            }
        }
        public void validarPosicaoDeOrigem(Posicao pos)
        {
            if (tab.peca(pos) == null)
            {
                throw new TabuleiroException("Não existe peça na posicao na posicao selecionada!");
            }
            if (jogadorAtual != tab.peca(pos).cor)
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
            if (!tab.peca(origem).movimentoPossivel(destino))
            {
                throw new TabuleiroException("Posicao de destino invalida!");
            }
        }
        private void mudaJogador()
        {
            if (jogadorAtual == Cor.white)
            {
                jogadorAtual = Cor.black;
            }
            else
            {
                jogadorAtual = Cor.white;
            }
        }
        public HashSet<Peca> pecasCapturadas(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in capturadas)
            {
                if(x.cor == cor)
                {
                    aux.Add(x);
                }
            }
            return aux;
        }
        public HashSet<Peca> pecasEmJogo(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in pecas)
            {
                if (x.cor == cor)
                {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(pecasCapturadas(cor));
            return aux;
        }
        private Cor adversaria(Cor cor)
        {
            if(cor == Cor.white)
            {
                return Cor.black;
            }
            else
            {
                return Cor.white;
            }
        }
        private Peca rei(Cor cor)
        {
            foreach (Peca x in pecasEmJogo(cor))
            {
                if(x is rei)
                {
                    return x;
                }
            }
            return null;
        }
        public bool estaEmXeque(Cor cor)
        {
            Peca R = rei(cor);
            if(R == null)
            {
                throw new TabuleiroException("There's no " + cor + " king on the boad!");
            }
            foreach(Peca x in pecasEmJogo(adversaria(cor)))
            {
                bool[,] mat = x.movimentosPossiveis();
                if(mat[R.posicao.linha, R.posicao.coluna])
                {
                    return true;
                }
            }
            return false;
        }
        public bool testeXequeMate(Cor cor)
        {
            if (!estaEmXeque(cor))
            {
                return false;
            }
            foreach(Peca x in pecasEmJogo(cor))
            {
                bool[,] mat = x.movimentosPossiveis();
                for(int  i=0; tab.linhas>i; i++)
                {
                    for(int j=0; tab.colunas>j; j++)
                    {
                        if (mat[i, j])
                        {
                            Posicao origem = x.posicao;

                            Posicao destino = new Posicao(i, j);
                            Peca pecaCapturada = executaMovimento(origem, destino);
                            bool testeXeque = estaEmXeque(cor);
                            desfazMovimento(origem, destino, pecaCapturada);
                            if (!testeXeque)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }
        public void colocarNovaPeca(char coluna, int linha, Peca peca)
        {
            tab.colocarPeca(peca, new PosicaoXadrex(coluna, linha).toPosicao());
            pecas.Add(peca);
        }
        private void colocarPecas() {
            //white
            colocarNovaPeca('a', 1, new torre(tab, Cor.white));
            colocarNovaPeca('b', 1, new cavalo(tab, Cor.white));
            colocarNovaPeca('c', 1, new bishop(tab, Cor.white));
            colocarNovaPeca('d', 1, new dama(tab, Cor.white));
            colocarNovaPeca('e', 1, new rei(tab, Cor.white, this));
            colocarNovaPeca('f', 1, new bishop(tab, Cor.white));
            colocarNovaPeca('g', 1, new cavalo(tab, Cor.white));
            colocarNovaPeca('h', 1, new torre(tab, Cor.white));
            colocarNovaPeca('a', 2, new pawn(tab, Cor.white));
            colocarNovaPeca('b', 2, new pawn(tab, Cor.white));
            colocarNovaPeca('c', 2, new pawn(tab, Cor.white));
            colocarNovaPeca('d', 2, new pawn(tab, Cor.white));
            colocarNovaPeca('e', 2, new pawn(tab, Cor.white));
            colocarNovaPeca('f', 2, new pawn(tab, Cor.white));
            colocarNovaPeca('g', 2, new pawn(tab, Cor.white));
            colocarNovaPeca('h', 2, new pawn(tab, Cor.white));
            //black
            colocarNovaPeca('a', 8, new torre(tab, Cor.black));
            colocarNovaPeca('b', 8, new cavalo(tab, Cor.black));
            colocarNovaPeca('c', 8, new bishop(tab, Cor.black));
            colocarNovaPeca('d', 8, new dama(tab, Cor.black));
            colocarNovaPeca('e', 8, new rei(tab, Cor.black, this));
            colocarNovaPeca('f', 8, new bishop(tab, Cor.black));
            colocarNovaPeca('g', 8, new cavalo(tab, Cor.black));
            colocarNovaPeca('h', 8, new torre(tab, Cor.black));
            colocarNovaPeca('a', 7, new pawn(tab, Cor.black));
            colocarNovaPeca('b', 7, new pawn(tab, Cor.black));
            colocarNovaPeca('c', 7, new pawn(tab, Cor.black));
            colocarNovaPeca('d', 7, new pawn(tab, Cor.black));
            colocarNovaPeca('e', 7, new pawn(tab, Cor.black));
            colocarNovaPeca('f', 7, new pawn(tab, Cor.black));
            colocarNovaPeca('g', 7, new pawn(tab, Cor.black));
            colocarNovaPeca('h', 7, new pawn(tab, Cor.black));

        }
    }
}
