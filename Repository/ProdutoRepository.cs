using exportacao_produtos.Dominio.Contratos;
using exportacao_produtos.Dominio.Entidades;

namespace exportacao_produtos.Repository
{
    internal class ProdutoRepository : IProdutoRepository
    {
        private static List<Produto> _produtos = new List<Produto>();
        public void Add(Produto produto)
        {
            _produtos.Add(produto);
        }

        public Produto Busca(int id)
        {
            var busca = _produtos.Find(x => x.Id == id);
            return busca;
        }

        public void Delete(int id)
        {
            var busca = Busca(id);
            _produtos.Remove(busca);
        }

        public List<Produto> GetAll()
        {
            return _produtos;
        }
    }
}
