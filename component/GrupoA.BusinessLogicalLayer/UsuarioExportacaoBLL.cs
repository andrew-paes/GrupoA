using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using GrupoA.DataAccess.ADO;
using GrupoA.BusinessObject;
using System.Transactions;

namespace GrupoA.BusinessLogicalLayer
{
    public class UsuarioExportacaoBLL : BaseBLL
    {
        /// <summary>
        /// Metodo que exporta dados atualizados do cliente para web service.
        /// </summary>
        public void ExportarUsuarios()
        {
            UsuarioADO usuarioADO = new UsuarioADO();
            
            // Busca sincronizacao = 1 (true)
            IEnumerable<Usuario> usuarios = usuarioADO.CarregaUsuariosParaExportacao(1);

            ComumicacaoComWebService(usuarios, System.Configuration.ConfigurationManager.AppSettings["ChaveSistema"].ToString());
        }

        /// <summary>
        /// Metodo que faz a comunicação com o web service do cliente
        /// </summary>
        /// <param name="pedidos">Lista de pedidos a ser enviada para o cliente</param>
        private void ComumicacaoComWebService(IEnumerable<Usuario> usuarios, String chave)
        {
            foreach (Usuario usuario in usuarios)
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    // TODO: Chamar Web Service
                    usuario.CodigoUsuario = "1234";
                    AtualizaPedidoWebService(usuario, chave);

                    scope.Complete();
                }
            }
        }


        /// <summary>
        /// Metodo que atualiza dados de resposta do web service do cliente
        /// </summary>
        /// <param name="pedido">Usuario para ser atualizado no banco</param>
        private void AtualizaPedidoWebService(Usuario usuario, String chave)
        {
            // Atualiza dados tabela de usuario
            UsuarioADO usuarioADO = new UsuarioADO();
            usuarioADO.Atualizar(usuario, chave);


            // Atualiza dados tabela de usuario controle
            UsuarioControleADO usuarioControleADO = new UsuarioControleADO();

            UsuarioControle usuarioControle = new UsuarioControle();
            usuarioControle.UsuarioId = usuario.UsuarioId;
            usuarioControle.RealizarSincronizacao = false;
            usuarioControle.DataHoraUltimaSincronia = DateTime.Now;

            usuarioControleADO.Atualizar(usuarioControle);
        }
    }
}
