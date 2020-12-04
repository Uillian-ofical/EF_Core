using EfCore.Contexts;
using EfCore.Domains;
using EfCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EfCore.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        // criando a instancia da conexão com o banco, e definindo que o arquivo será apenas para leitura
        private readonly PedidoContext _context;

        public ProdutoRepository()
        {
            _context = new PedidoContext();
        }

        /// <summary>
        ///     Lista todos os nossos produtos cadastrados no nosso sistema
        /// </summary>
        /// <returns>Lista com todos os produtos no sistemas</returns>
        public List<Produto> ListarPodutos()
        {
            return _context.Produtos.ToList();
        }

        /// <summary>
        ///     Buscar informações do produto informado
        /// </summary>
        /// <param name="_idProduto">Código de identificação do produto</param>
        /// <returns>Dados do produto</returns>
        public Produto BuscarProdutoId(Guid _idProduto)
        {
            try
            {
                return _context.Produtos.FirstOrDefault(prd => prd.Id == _idProduto);

            }catch(Exception _e){

                throw new Exception(_e.Message);
            }
        }

        /// <summary>
        ///     Buscar produtos de acordo com o nome do produto
        /// </summary>
        /// <param name="_nomeProduto">Nome do produto</param>
        /// <returns>Lista com os produtos</returns>
        public List<Produto> BuscarProdutoNome(string _nomeProduto)
        {
            try
            {
                return _context.Produtos.Where(prd => prd.Nome.Contains(_nomeProduto)).ToList();

            }
            catch (Exception _e)
            {

                throw new Exception(_e.Message);
            }
        }

        /// <summary>
        ///     Adicionando um novo produto ao nosso sistema
        /// </summary>
        /// <param name="_produto"></param>
        /// <returns>Dados do produto cadastrado</returns>
        public Produto CadastrarProduto(Produto _produto)
        {
            try
            {
                _context.Produtos.Add(_produto);

                _context.SaveChanges();
            
            }catch(Exception e){

                throw new Exception(e.Message);
            }

            return _produto;
        }

        public Produto AlterarProduto(Produto _produto)
        {
            try
            {
                Produto _produtoBuscado = BuscarProdutoId(_produto.Id);

                _produtoBuscado.Nome = _produto.Nome;
                _produtoBuscado.Preco = _produto.Preco;
                _produtoBuscado.UrlImagem = _produto.UrlImagem;

                _context.Produtos.Update(_produtoBuscado);
                _context.SaveChanges();

                return _produtoBuscado;

            }catch(Exception _e){

                throw new Exception(_e.Message);
            }
        }

        /// <summary>
        ///     Excluindo produto especifico
        /// </summary>
        /// <param name="_idProduto">Código de identificação do produto</param>
        public void ExcluirProduto(Guid _idProduto)
        {
            try
            {
                Produto _produtoBuscado = BuscarProdutoId(_idProduto);

                if (_produtoBuscado == null)
                {
                    throw new Exception("Produto não encontrado");
                }

                _context.Produtos.Remove(_produtoBuscado);
                _context.SaveChanges();

            }
            catch (Exception _e)
            {

                throw new Exception(_e.Message);
            }
        }
    }
}
