using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EfCore.Domains;
using EfCore.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EfCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private readonly PedidoRepository _repository;

        public PedidoController()
        {
            _repository = new PedidoRepository();
        }

        /// <summary>
        ///     Método para listar todos os pedidos do nosso sistema
        /// </summary>
        /// <returns>Lista de pedidos efetuados</returns>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var _pedidos = _repository.ListarPedidos();

                if(_pedidos.Count == 0)
                {
                    return NoContent();
                }

                return Ok(_pedidos);

            }catch (Exception){

                throw;
            }
        }

        /// <summary>
        ///     Método para buscar os dados de um pedido especifico
        /// </summary>
        /// <param name="id">Código de identificação do pedido</param>
        /// <returns>Dados do pedido buscado</returns>
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            try
            {
                var _pedido = _repository.BuscarPorId(id);

                if (_pedido == null)
                {
                    return NotFound();
                }

                return Ok(_pedido);

            }catch (Exception){

                throw;
            }
        }

        /// <summary>
        ///     Método para cadastro de um pedido
        /// </summary>
        /// <param name="_pedidosItens">Produtos no qual você solicitará no seu pedido</param>
        /// <returns>Dados referente ao pedido cadastrado</returns>
        [HttpPost]
        public IActionResult Post(List<PedidoItem> _pedidosItens)
        {
            try
            {
                Pedido _pedido = _repository.CadastrarProduto(_pedidosItens);

                return Ok(_pedido);

            }catch (Exception){

                throw;
            }
        }

        // PUT api/<PedidoController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PedidoController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
