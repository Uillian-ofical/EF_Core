using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EfCore.Domains
{
    // definir abstractic siginifica dizer que a classe não pode ser instanciada
    public abstract class BaseDomain
    {
        // Para questão de segurança da aplicação é usado o Guid
        // para evitar que seja identificado o id do objeto e evitar infrações de segurança
        [Key]
        public Guid Id { get; private set; }
                              // definindo como private para que somente no método construtor
                              // o id possa ser setado;


        public BaseDomain()
        {
            Id = Guid.NewGuid();
        }
    }
}
