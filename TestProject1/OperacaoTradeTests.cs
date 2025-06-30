using _20250627;

namespace TestProject1
{
    [TestClass]
    public class OperacaoTradeTests
    {
        [TestMethod]
        public void TestCaso1()
        {
            var cenario = new Cenario("Chuvoso", 120, 20);

            var operacaoTrade = new OperacaoTrade(ETipoPreco.CompraVariavel, 157, cenario, 200, 3, null);

            operacaoTrade.Calcular();

            Assert.AreEqual(operacaoTrade.Ajuste, false);
            Assert.AreEqual(31400, operacaoTrade.ValorNominal);
            Assert.AreEqual(4000, operacaoTrade.MTM);
        }

        [TestMethod]
        public void TestCaso2()
        {
            var operacaoTrade = new OperacaoTrade(ETipoPreco.CompraVariavel, 157, null, 200, 8, null);

            operacaoTrade.Calcular();

            Assert.AreEqual(operacaoTrade.Ajuste, false);
            Assert.AreEqual(31400, operacaoTrade.ValorNominal);
            Assert.AreEqual(0, operacaoTrade.MTM);
        }

        [TestMethod]
        public void TestCaso3()
        {
            var operacaoTrade = new OperacaoTrade(ETipoPreco.VendaFixo, 157, null, 200, 8, 120);

            operacaoTrade.Calcular();
            operacaoTrade.AjustarVolume(300);

            Assert.AreEqual(operacaoTrade.Ajuste, true);
            Assert.AreEqual(36000, operacaoTrade.ValorNominal);
            Assert.AreEqual(-11100, operacaoTrade.MTM);
        }
    }
}
