using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using GrupoA.BusinessLogicalLayer.Helper;
using GrupoA.BusinessObject;
using GrupoA.DataAccess;
using GrupoA.DataAccess.ADO;

namespace GrupoA.BusinessLogicalLayer
{
    /// <summary>
    /// Classe que abstrai as regras de negócio referentes a usuários.
    /// </summary>    
    public class UsuarioRevistaBLL : BaseBLL
    {
        #region Propriedades

        private IUsuarioRevistaDAL _usuarioRevistaDal;
        private IUsuarioRevistaDAL UsuarioRevistaDal
        {
            get { return _usuarioRevistaDal ?? (_usuarioRevistaDal = new UsuarioRevistaADO()); }
        }

        #endregion

        #region Métodos

        /// <summary>
        /// 
        /// </summary>
        /// <param name="usuarioRevistaBO"></param>
        public void Inserir(UsuarioRevista usuarioRevistaBO, String caminhotemplate)
        {
            UsuarioRevistaDal.Inserir(usuarioRevistaBO);

            this.EnviarEmailUsuario(usuarioRevistaBO.Usuario, caminhotemplate, usuarioRevistaBO);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="usuarioRevista"></param>
        public void Atualizar(UsuarioRevista usuarioRevista, String caminhotemplate)
        {
            UsuarioRevistaDal.Atualizar(usuarioRevista);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="usuarioRevista"></param>
        public void Excluir(UsuarioRevista usuarioRevista)
        {
            UsuarioRevistaDal.Excluir(usuarioRevista);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="usuarioRevista"></param>
        /// <returns></returns>
        public UsuarioRevista Carregar(UsuarioRevista usuarioRevista)
        {
            return UsuarioRevistaDal.Carregar(usuarioRevista);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        public List<UsuarioRevista> CarregarAssinaturasValidasPorUsuario(Usuario entidade)
        {
            return UsuarioRevistaDal.CarregarAssinaturasValidasPorUsuario(entidade);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="usuarioBO"></param>
        /// <param name="caminhotemplate"></param>
        /// <returns></returns>
        public Boolean EnviarEmailUsuario(Usuario usuarioBO, String caminhotemplate)
        {
            return EnviarEmailUsuario(usuarioBO, caminhotemplate, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="usuarioBO"></param>
        /// <param name="caminhotemplate"></param>
        /// <returns></returns>
        public Boolean EnviarEmailUsuario(Usuario usuarioBO, String caminhotemplate, UsuarioRevista usuarioRevistaBO)
        {
            string subject = String.Empty;
            string emailRemetente = String.Empty;

            try
            {
                if (usuarioBO != null && usuarioBO.UsuarioId > 0)
                {
                    usuarioBO = new UsuarioBLL().CarregarUsuario(usuarioBO);

                    Dictionary<String, String> dicionarioDados = new Dictionary<String, String>();
                    //dicionarioDados.Add("Nome", usuarioBO.NomeUsuario);
                    dicionarioDados.Add("CaminhoSite", System.Configuration.ConfigurationManager.AppSettings["CaminhoSite"].ToString());

                    if (usuarioRevistaBO.Revista != null && usuarioRevistaBO.Revista.RevistaId > 0)
                    {
                        dicionarioDados.Add("NomeRevista", usuarioRevistaBO.Revista.NomeRevista);
                        dicionarioDados.Add("DataAssinaturaInicio", usuarioRevistaBO.DataInicioAssinatura.ToString("dd/MM/yyyy"));
                        dicionarioDados.Add("DataAssinaturaFim", usuarioRevistaBO.DataFimAssinatura.ToString("dd/MM/yyyy"));

                        switch (usuarioRevistaBO.Revista.RevistaId)
                        {
                            case 1:
                                emailRemetente = GrupoA.GlobalResources.GrupoA_Resource.EmailSACBMJ;
                                subject = "Acesso liberado | Revista BMJ";
                                break;
                            case 2:
                            case 3:
                            case 4:
                                emailRemetente = GrupoA.GlobalResources.GrupoA_Resource.EmailSACPatio;
                                subject = "Acesso liberado | Revista Pátio";
                                break;
                            default:
                                break;
                        }
                    }

                    StringBuilder templateEmail = new EmailHelper().PopulaTemplateEmail(dicionarioDados, caminhotemplate);

                    new EmailHelper().EnviarEmail(emailRemetente, usuarioBO.EmailUsuario, subject, templateEmail);

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion
    }
}