using Microsoft.Extensions.DependencyInjection;
using exportacao_produtos.Dominio.Contratos;
using exportacao_produtos.Dominio.Entidades;
using exportacao_produtos.Repository;
using exportacao_produtos.Services;
using System.Threading.Channels;
using System.Text.Json;
using exportacao_produtos.Dominio.Enumeradores;

namespace exportacao_produtos
{
    internal class Program
    {
        static void Main(string[] args)
        {

            var services = new ServiceCollection();
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<IProdutoService, ProdutoService>();
            var provider = services.BuildServiceProvider();
            var produtoService = provider.GetService<IProdutoService>();
            var produtoRepository = provider.GetService<IProdutoRepository>();

            Console.WriteLine("Bem vindo ao sistema.");
            bool run = true;
            do {
                Console.WriteLine("**Menu**");
                Console.WriteLine("-1) Adicionar Produto.\n" +
                                  "-2) Deletar Produto\n" +
                                  "-3) Buscar Produto\n" +
                                  "-4) Exportar\n" +
                                  "-5) Importar\n" +
                                  "-0) Parar");
                Console.Write("Informe: ");
                Enum.TryParse(Console.ReadLine(), out Menu result);
                switch (result)
                {
                    case (Menu.Parar):
                        run = false; 
                        break;
                    case (Menu.Criar):
                        Console.Write("Nome do Produto: ");
                        string nome = Console.ReadLine();
                        Console.Write("Lote: ");
                        int lote = int.Parse(Console.ReadLine());
                        Console.Write("Validade: ");
                        DateTime.TryParse(Console.ReadLine(), out DateTime validade);
                        produtoService.Create(nome, validade, lote);
                        Console.WriteLine("Produto criado e adicionado ao banco de dados.\n");
                        break;
                    case (Menu.Deletar):
                        Console.Write("Informe o id do Produto a ser excluido, caso tenha duvida, peça a lista.");
                        Console.WriteLine("Deseja listar?(y/n): ");
                        char opt = char.Parse(Console.ReadLine());
                        if (opt == 'y')
                        {
                            var produtos = produtoRepository.GetAll();
                            for (int i = 0; i < produtos.Count; i++)
                            {
                                Console.WriteLine($"Nome: {produtos[i].Nome}\n" +
                                                  $"Id: {produtos[i].Id}\n...");
                            }
                        }
                        Console.Write("Id: ");
                        int id = int.Parse(Console.ReadLine());
                        produtoService.Delete(id);
                        break;
                    case (Menu.Buscar):
                        Console.Write("Informe o ID do produto: ");
                        id = int.Parse(Console.ReadLine());
                        var produto = produtoService.Busca(id);
                        Console.WriteLine($"Produto: {produto.Nome}\n" +
                                          $"Id: {produto.Id}\n" +
                                          $"Lote: {produto.Lote}\n" +
                                          $"Validade: {produto.Validade}");
                        break;
                    case (Menu.Exportar):
                        Console.Write("Json, Csv ou Txt?: ");
                        Enum.TryParse(Console.ReadLine(), out Tipos result1);
                        var tipo = " ";
                        switch (result1)
                        {
                            case Tipos.Txt:
                                tipo = "txt";
                                break;
                            case Tipos.Json:
                                tipo = "json";
                                break;
                            case Tipos.Csv:
                                tipo = "csv";
                                break;
                        }
                        produtoService.Export(tipo);
                        Console.WriteLine("Pronto! Produtos exportados para o diretório padrão.");
                        break;
                    case (Menu.Importar):
                        Console.WriteLine("Informe o diretório do arquivo");
                        string path = Console.ReadLine();
                        produtoService.Import(path);
                        Console.WriteLine("Produtos importados para o Banco de Dados");
                        break;
                }

            } while (run == true);



            //Falta serialização
        }
    }
}