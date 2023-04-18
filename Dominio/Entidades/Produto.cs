namespace exportacao_produtos.Dominio.Entidades
{
    internal class Produto
    {
        static private int _id = 0;
        private int id { get; set; }
        public int Id { get { return id; } }
        public string Nome { get; set; }
        public DateTime Validade { get; set; }
        public int Lote { get; set; }

        public Produto()
        {
            _id++;
            id = _id;
        }
        public Produto(string nome, DateTime validade, int lote) : this()
        {
            Nome = nome;
            Validade = validade;
            Lote = lote;
        }
    }
}
