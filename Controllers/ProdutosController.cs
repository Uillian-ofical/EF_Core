using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using EfCore.Domains;
using EfCore.Interfaces;
using EfCore.Repositories;
using EfCore.Utils;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EfCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutoRepository _repository;

        public ProdutosController()
        {
            _repository = new ProdutoRepository();
        }

        /// <summary>
        ///     Método para listar os produtos inseridos no sistema
        /// </summary>
        /// <returns>Todos os produtos cadastrados</returns>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var _produtos = _repository.ListarPodutos();
                
                // Verificando se existe produtos no repository
                if(_produtos.Count == 0)
                {
                    return NoContent();
                }

                return Ok(new {

                    totalCount = _produtos.Count,
                    data = _produtos
                }) ;
            }
            catch (Exception _e){

                return BadRequest(_e.Message);
                throw;
            }

        }

        /// <summary>
        ///     Método para buscar os dados referente a um produto especifico
        /// </summary>
        /// <param name="id">O código identificador do produto</param>
        /// <returns>Dados pertinentes ao produto pesquisado</returns>
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            try
            {
                Produto _produto = _repository.BuscarProdutoId(id);

                if(_produto == null)
                {
                    return NoContent();
                }

                Moeda dolar = new Moeda();

                return Ok(new {
                    _produto,
                    valorDolar = _produto.Preco / dolar.GetDolarValue()
                });
            }
            catch (Exception _e){

                return BadRequest(_e.Message);
            }
        }

        /// <summary>
        ///     Método para cadastro de um novo produto
        /// </summary>
        /// <param name="_produto">Produto capturado através do formulário</param>
        /// <returns>Dados referente ao produto cadastrado</returns>
        [HttpPost]
        public IActionResult Post([FromForm] Produto _produto)
        {
            try
            {
                if(_produto.Imagem != null)
                {
                    var _urlImg = UploadFiles.Local(_produto.Imagem);

                    _produto.UrlImagem = _urlImg;
                }

                _repository.CadastrarProduto(_produto);

                return Ok(_produto);
            }
            catch (Exception _e){

                return BadRequest(_e.Message);
            }
        }

        // PUT api/<ProdutosController>/5
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromForm] Produto _produto)
        {
            try
            {
                Produto _produtoBuscado = _repository.BuscarProdutoId(id);

                if(_produtoBuscado == null)
                {
                    return NotFound();
                }

                if (_produto.Imagem != null)
                {
                    var _urlImg = UploadFiles.Local(_produto.Imagem);

                    _produto.UrlImagem = _urlImg;
                }

                //_produto.Id = id;
                _repository.AlterarProduto(_produto);

                return Ok(_produto);
            }
            catch (Exception _e){

                return BadRequest(_e.Message);
            }
        }

        // DELETE api/<ProdutosController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                Produto _produtoBuscado = _repository.BuscarProdutoId(id);

                if(_produtoBuscado == null)
                {
                    return NotFound();
                }

                _repository.ExcluirProduto(id);

                return Ok(_produtoBuscado);
            }
            catch (Exception _e){

                return BadRequest(new {
                    statusCode = 400,
                    error = "Não foi possivel realizar esta operação"
                });
            }
        }
    }
}
