using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EfCore.Domains
{
    public class Pedido : BaseDomain
    {
        public string Status { get; set; }
        public DateTime OrderDate { get; set; }

        // relacionamente com a tabela de pedidos de itend (1 > N)
        public List<PedidoItem> PedidosItens { get; set; }

        public Pedido()
        {
            PedidosItens = new List<PedidoItem>();
        }
    }
}
