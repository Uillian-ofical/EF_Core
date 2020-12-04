using EfCore.Contexts;
using EfCore.Domains;
using EfCore.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EfCore.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly PedidoContext _context;

        public PedidoRepository()
        {
            _context = new PedidoContext();
        }

        public List<Pedido> ListarPedidos()
        {
            try
            {

                return _context.Pedidos.ToList();

            }catch (Exception _e){

                throw new Exception(_e.Message);
            }
        }

        public Pedido BuscarPorId(Guid _idPedido)
        {
            try
            {

                return _context.Pedidos.Include(prd => prd.PedidosItens).ThenInclude(prd => prd.Produto)
                                            .FirstOrDefault(prd => prd.Id == _idPedido);

            }catch (Exception _e){

                throw new Exception(_e.Message);
            }
        }

        public Pedido CadastrarProduto(List<PedidoItem> _pedidosItens)
        {
            try
            {
                // criando um novo pedido
                Pedido _pedido = new Pedido
                {
                    Status = "Pedido efetuado",
                    OrderDate = DateTime.Now
                };

                // percorre a lista de pedidos de itens e adiciona a lista de _pedidosItens
                foreach(var _item in _pedidosItens)
                {
                    _pedido.PedidosItens.Add(new PedidoItem {

                        IdPedido = _pedido.Id, // _pedido criado acima
                        IdProduto = _item.IdProduto,
                        Quantidade = _item.Quantidade
                    });
                }

                // adicionando o pedido ao meu context
                _context.Pedidos.Add(_pedido);

                // passando as alterações do context para o banco de dados
                _context.SaveChanges();

                return _pedido;

            }catch (Exception _e){

                throw new Exception(_e.Message);
            }
        }

    }
}
