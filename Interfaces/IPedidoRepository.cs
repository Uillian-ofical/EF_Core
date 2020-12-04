using EfCore.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EfCore.Interfaces
{
    public interface IPedidoRepository
    {
        List<Pedido> ListarPedidos();
        Pedido BuscarPorId(Guid _idPedido);

        Pedido CadastrarProduto(List<PedidoItem> _pedidosItens);
    }
}
