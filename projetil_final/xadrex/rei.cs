using tabuleiro;

namespace xadrex
{
    class rei : Peca
    {
        public rei(Tabuleiro tab, Cor cor) : base(tab, cor)
        {}
        public override string ToString()
        {
            return "R";
        }

    }
}
