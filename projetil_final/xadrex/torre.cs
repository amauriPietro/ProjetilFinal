using tabuleiro;

namespace xadrex
{
    class torre : Peca
    {
        public torre(Tabuleiro tab, Cor cor) : base(tab, cor)
        { }
        public override string ToString()
        {
            return "T";
        }

    }
}
