using exportacao_produtos.Dominio.Contratos;
using exportacao_produtos.Dominio.Entidades;
namespace exportacao_produtos.Services
{
    internal class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _repository;
        public ProdutoService(IProdutoRepository repos)
        {
            _repository = repos;
        }
        public Produto Create(string nome, DateTime validade, int lote)
        {
            Produto prod = new Produto(nome, validade, lote);
            _repository.Add(prod);
            return prod;
        }

        public Produto Busca(int id)
        {
           var busca = _repository.Busca(id);
           return busca;
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public void Export(string tipo)
        {
            string path = $"C:\\Users\\João Emanuel\\source\\repos\\exportacao_produtos\\Files\\file.{tipo}";
            FileStream fs = new FileStream(path, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            var produtos = _repository.GetAll();
            for (int i = 0; i < produtos.Count(); i++)
            {
                var produto = Busca(i + 1);
                sw.WriteLine($"sep= ;\n{produto.Nome};{produto.Lote};{produto.Validade};");
            }
            sw.Close();
            fs.Close();
        }
        public void Import(string path)
        {
            var fs = new FileStream(path, FileMode.Open);
            var sr = new StreamReader(fs);
            char sep = ' ';
            while (!sr.EndOfStream)
            {
                string linha = sr.ReadLine();
                if (linha.StartsWith("sep="))
                {
                    sep = linha[linha.Length - 1];
                    continue;
                }
                string[] objects = linha.Split(sep);
                Console.WriteLine($"Importado!\n {linha}.");
                Create(objects[0], DateTime.Parse(objects[2]), int.Parse(objects[1]));
            }

        }
    }
}
