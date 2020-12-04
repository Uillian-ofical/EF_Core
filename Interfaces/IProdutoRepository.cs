using EfCore.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EfCore.Interfaces
{
    public interface IProdutoRepository
    {
        List<Produto> ListarPodutos();
        Produto BuscarProdutoId(Guid _idProduto);
        List<Produto> BuscarProdutoNome(string _nomeProduto);
        Produto CadastrarProduto(Produto _produto);
        Produto AlterarProduto(Produto _produto);
        void ExcluirProduto(Guid _idProduto);
    }
}
