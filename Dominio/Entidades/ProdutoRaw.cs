using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exportacao_produtos.Dominio.Entidades
{
    internal class ProdutoRaw
    {
        public int Id { get; set; }
        public string Nome { get; set;}
        public DateTime Validade { get; set; }
        public int lote { get; set; }
    }
}
