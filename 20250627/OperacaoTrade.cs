using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20250627
{
    public class OperacaoTrade
    {
        public decimal? Pld { get; private set; }
        public decimal? Preco { get; private set; }
        public decimal Volume { get; private set; }
        public int DiaUtil { get; }
        public ETipoPreco TipoPreco { get; private set; }
        public decimal ValorNominal { get; private set; }
        public decimal MTM { get; private set; }
        public Cenario? Cenario { get; private set; }
        public bool Ajuste { get; private set; }

        public OperacaoTrade(ETipoPreco tipoPreco, decimal? pld, Cenario? cenario, decimal volume, int diaUtil, decimal? preco)
        {
            TipoPreco = tipoPreco;
            Pld = pld;
            Cenario = cenario;
            Volume = Math.Round(volume, 3);
            DiaUtil = diaUtil;
            Preco = preco;
            Ajuste = false;
        }

        public void AjustarVolume(decimal volume)
        {
            Volume = Math.Round(volume, 3);
            Ajuste = true;
            Calcular();
        }

        private decimal GetPrecoFinal()
        {
            if (TipoPreco == ETipoPreco.CompraVariavel)
                return Pld ?? 0m;
            else
                return Preco ?? 0m;
        }

        public void Calcular()
        {
            decimal precoFinal = GetPrecoFinal();

            ValorNominal = Volume * precoFinal;

            if (Pld == null)
            {
                MTM = Volume * (precoFinal - ((Cenario?.Preco ?? 0m) + (Cenario?.Spread ?? 0m))) * GetFator();
                return;
            }
            
            if (DiaUtil <= 7)
            {
                MTM = Volume * (precoFinal - ((Pld ?? 0m) + (Cenario?.Spread ?? 0m))) * GetFator();
            }
            else
            {
                MTM = Volume * (precoFinal - (Pld ?? 0m)) * GetFator();
            }
        }

        private int GetFator() {
            switch (TipoPreco) {
                case ETipoPreco.CompraVariavel:
                    return -1;

                case ETipoPreco.VendaFixo:
                    return 1;
                
                default:
                    return 1;
            }
        }
    }

}
