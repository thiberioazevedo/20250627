namespace _20250627
{
    public class Cenario
    {
        public Cenario(string descricao, decimal preco, decimal spread)
        {
            Descricao = descricao;
            Preco = preco;
            Spread = spread;
        }

        public string Descricao { get; }
        public decimal Preco { get; }
        public decimal Spread { get; }
    }
}