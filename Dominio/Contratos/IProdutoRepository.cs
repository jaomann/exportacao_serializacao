using exportacao_produtos.Dominio.Entidades;

namespace exportacao_produtos.Dominio.Contratos
{
    internal interface IProdutoRepository
    {
        void Add(Produto produto);
        void Delete(int id);
        Produto Busca(int id);
        List<Produto> GetAll();
    }
}
