using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GrupoA.PaymentGateway
{
    /// <summary>
    /// Classe que abstrai os dados do cliente.
    /// </summary>
    public class ClienteDTO
    {
        /// <summary>
        /// Nome do cliente.
        /// </summary>
        public virtual string NomeDoCliente { get; set; }

        /// <summary>
        /// CPF ou CNPJ do portador do cartão
        /// </summary>
        public virtual string CadastroNacional { get; set; }

        /// <summary>
        /// Logradouro do endereço.
        /// </summary>
        public virtual string LogradouroDoEndereco { get; set; }

        /// <summary>
        /// Número do endereço.
        /// </summary>
        public virtual string NumeroDoEndereco { get; set; }

        /// <summary>
        /// Complemento do endereço.
        /// </summary>
        public virtual string ComplementoDoEndereco { get; set; }

        /// <summary>
        /// CEP do endereço.
        /// </summary>
        public virtual string Cep { get; set; }

        /// <summary>
        /// Municipio do endereço.
        /// </summary>
        public virtual string Municipio { get; set; }

        /// <summary>
        /// Unidade Federativa da região do endereço.
        /// </summary>
        public virtual string UF { get; set; }

        /// <summary>
        /// País do endereço.
        /// </summary>
        public virtual string Pais { get; set; }

        /// <summary>
        /// Número do telefone do cliente (sem DDD).
        /// </summary>
        public virtual string TelefoneDoClienteNumero { get; set; }

        /// <summary>
        /// Número do DDD do telefone do cliente.
        /// </summary>
        public virtual string TelefoneDoClienteDdd { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string Email { get; set; }

        /// <summary>
        /// “1” para PF; “2” para PJ
        /// </summary>
        public virtual string TipoCliente { get; set; }
    }
}
