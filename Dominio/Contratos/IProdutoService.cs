using exportacao_produtos.Dominio.Entidades;

namespace exportacao_produtos.Dominio.Contratos
{
    internal interface IProdutoService
    {
        Produto Create(string nome, DateTime validade, int lote);
        void Delete(int id);
        Produto Busca(int id);
        void Export(string tipo);
        void Import(string path);
    }
}
