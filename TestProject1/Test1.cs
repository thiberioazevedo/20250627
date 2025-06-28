using _20250627;

namespace TestProject1
{
    [TestClass]
    public class Test1
    {
        [TestMethod]
        public void TestMethod1()
        {
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

            var cenario = new Cenario("Chuvoso", 120, 20);

            var operacaoTrade = new OperacaoTrade(ETipoPreco.Variavel, 157, cenario, 200, 3, null);

            operacaoTrade.Calcular();

            Assert.AreEqual(operacaoTrade.Ajuste, false);
            Assert.AreEqual(31400, operacaoTrade.ValorNominal);
            Assert.AreEqual(4000, operacaoTrade.MTM);
        }

        [TestMethod]
        public void TestMethod2()
        {
            /*
            ### Caso 2

            * Tipo: Compra Variável
            * PLD Fechado: R\$ 157,00/MWh
            * Volume: 200 MWh
            * Ajuste: Não
            * Dia Útil: 8 DU

            **Resultado Esperado**:

            * Valor Nominal: `R$ 31.400,00`
            * MTM: `R$ 0,00`
            */

            var operacaoTrade = new OperacaoTrade(ETipoPreco.Variavel, 157, null, 200, 8, null);

            operacaoTrade.Calcular();

            Assert.AreEqual(operacaoTrade.Ajuste, false);
            Assert.AreEqual(31400, operacaoTrade.ValorNominal);
            Assert.AreEqual(0, operacaoTrade.MTM);
        }

        [TestMethod]
        public void TestMethod3()
        {
            /*
            ### Caso 3

            * Tipo: Venda Fixo
            * PLD Fechado: R\$ 157,00/MWh
            * Preço Fixo: R\$ 120,00/MWh
            * Ajuste de Volume: 300 MWh
            * Dia Útil: 8 DU

            **Resultado Esperado**:

            * Valor Nominal: `R$ 36.000,00`
            * MTM: `R$ -11.100,00`
            */

            var operacaoTrade = new OperacaoTrade(ETipoPreco.Fixo, 157, null, 200, 8, 120);

            operacaoTrade.Calcular();

            operacaoTrade.AjustarVolume(300);

            Assert.AreEqual(operacaoTrade.Ajuste, true);
            Assert.AreEqual(36000, operacaoTrade.ValorNominal);
            Assert.AreEqual(-11100, operacaoTrade.MTM);
        }
    }
}
