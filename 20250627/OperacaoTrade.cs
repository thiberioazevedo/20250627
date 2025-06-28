using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20250627
{
    /*
     * 
Uma **Operação** no mercado ACL pode ser de **Compra** ou **Venda**, e deve conter:

* Um **preço** (fixo ou variável)
* Um **volume** (em MWh)
* Um ou mais **meses de fornecimento**
     */


    /*
     ### Caso 1

* Tipo: Compra Variável
* PLD Fechado: R\$ 157,00/MWh
* Cenário: Chuvoso — R\$ 120,00/MWh
* Spread: R\$ 20,00
* Volume: 200 MWh
* Ajuste: Não
* Dia Útil: 3 DU

**Resultado Esperado**:

* Valor Nominal: `R$ 31.400,00`
* MTM: `R$ 4.000,00`
     */
    public class OperacaoTrade
    {
        public ETipoPreco variavel;
        public decimal? Pld;


        public decimal? Preco { get; internal set; }  
        public decimal Volume { get; internal set; }
        public int DiaUtil { get; }
        public ETipoPreco TipoPreco { get; internal set; }
        //public IList<MesFornecimento> MesFornecimentoList { get; set; }
        public int ValorNominal { get; internal set; }
        public int MTM { get; internal set; }
        public Cenario? Cenario { get; internal set; }

        public bool Ajuste { get; internal set; }

        public OperacaoTrade(ETipoPreco tipoPreco, int? pld, Cenario? cenario, decimal volume, int diaUtil, decimal? preco)
        {
            TipoPreco = tipoPreco;
            Pld = pld;
            Cenario = cenario;
            Volume = volume;
            DiaUtil = diaUtil;
            Preco = preco;
        }
        /*
         * 
        | Situação                                           | Fórmula                                                        |
        | -------------------------------------------------- | -------------------------------------------------------------- |
        | Mês **sem PLD fechado**                            | `Volume * (Preço Final - (Preço Cenário + Spread do Cenário))` |
        | Até o **7º Dia Útil** após o PLD ser fechado       | `Volume * (Preço Final - (PLD + Spread do Cenário))`           |
        | A partir do **8º Dia Útil** após o PLD ser fechado | `Volume * (Preço Final - PLD)`       


        
#### Tipos de Preço

* **Fixo**: definido diretamente na operação.
* **Variável**: depende do valor do **PLD** (Preço de Liquidação das Diferenças), que é publicado ao final de cada mês.

        
                 */
        public void Calcular() {
            //if (Pld == null)  
            //{
            //    MTM = (int)(Volume * (GetPrecoFinal() - (Cenario.Preco + Cenario.Spread)));
            //    ValorNominal = (int)(Volume * GetPrecoFinal());
            //}

            if (Pld != null && DiaUtil < 8) {
                MTM = Math.Abs((int)(Volume * ((GetPrecoFinal() - (Pld + Cenario.Spread)))));
                ValorNominal = (int)(Volume * GetPrecoFinal());
            }

            if (Pld != null && DiaUtil >= 8)
            {
                MTM = Math.Abs((int)(Volume * ((GetPrecoFinal() - (Pld)))));
                ValorNominal = (int)(Volume * GetPrecoFinal());
            }
        }

        private decimal? GetPrecoFinal() {
            if (TipoPreco == ETipoPreco.Variavel)
                return (int)Pld;
            else
                return Preco;
        }

        public void AjustarVolume(int volume)
        {
            Volume = volume;
            Ajuste = true;
        }
    }
}
