using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EfCore.Domains
{
    public class Produto : BaseDomain
    {
        public string Nome { get; set; }
        public float Preco { get; set; }

        [NotMapped] // não mapeia a propriedade no banco de dados
        [JsonIgnore] // ignora o parametro no retorno do json
        public IFormFile Imagem { get; set; }
        public string UrlImagem { get; set; }


        // relacionamento com a tabela de pedido de iten (1 > N)
        public List<PedidoItem> PedidosItens { get; set; }
    }
}
