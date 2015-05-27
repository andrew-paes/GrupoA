using System;
using System.IO;
using System.Transactions;
using Ag2.Manager.Helper;
using Ag2.Manager.Module;
using Ag2.Manager.Module.Info;
using GrupoA.BusinessLogicalLayer;
using GrupoA.BusinessObject;
using GrupoA.GlobalResources;

/// <summary>
/// Summary description for DeleteUsuario
/// </summary>
public class DeleteEvento
{
    public DeleteEvento()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    /// <summary>
    /// M�todo configurado no Web.Config para ser executado quando o usu�rio executa a a��o de delete no manager
    /// </summary>
    /// <param name="module"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    public static DeleteRegisterInfo BeforeRegisterDelete(ManagerModuleStructure module, string id)
    {
        DeleteRegisterInfo ObjDelete = new DeleteRegisterInfo();
        ObjDelete.CanDelete = false; //SETADO COMO FALSE PARA ELE N�O EXECUTAR O DELETE PADRAO DO MANAGER

        bool exclusaoFisica = true;
        String nomeArquivo = "";

        EventoBLL eventoBLL = new EventoBLL();
        Evento evento = new Evento();
        evento.EventoId = Convert.ToInt32(id);
        evento = eventoBLL.CarregarEventoComDependencias(evento);

        using (TransactionScope scope = new TransactionScope())
        {
            eventoBLL.ExcluirEvento(evento);

            enviaEmailAtualizacao(evento); // Envia e-mail para usu�rios avisando sobre o cancelamento

            if (evento.EventoAlertas.Count == 0) // Caso o evento tenha alertas, n�o deve excluir fisicamente as imagens
            {
                if (evento.EventoImagens != null)
                {
                    foreach (EventoImagem eventoImagem in evento.EventoImagens)
                    {
                        nomeArquivo = eventoImagem.Arquivo.NomeArquivo;
                        //string pathFile = string.Concat(System.Web.HttpContext.Current.Server.MapPath(ConfigurationSettings.AppSettings["uploadRoot"].ToString()), "imagensEvento\\", nomeArquivo);
                        string pathFile = string.Concat(System.Web.HttpContext.Current.Server.MapPath(GrupoA_Resource.baseUrlUpload), GrupoA_Resource.PastaImagensEvento, nomeArquivo);
                        FileInfo info = new FileInfo(pathFile);

                        if (info.Exists)
                        {
                            info.Delete();
                        }
                        else
                        {
                            exclusaoFisica = false;
                        }
                    }
                }
                if (exclusaoFisica)
                {
                    scope.Complete();
                    Util.ShowMessage("Evento exclu�do com sucesso!", Ag2.Manager.Enumerator.typeMessage.Sucesso);
                }
                else
                {
                    throw new FileNotFoundException("N�o foi poss�vel encontrar o arquivo f�sico para exclus�o");
                }
            }
            else
            {
                //TODO: Deve enviar e-mails para quem tem alertas relacionados � estes eventos
                scope.Complete();
                Util.ShowMessage("Evento atualizado com sucesso!<BR><BR>Aten��o: em fun��o da exist�ncia de alertas, o Evento foi inativado.", Ag2.Manager.Enumerator.typeMessage.Sucesso);
            }
        }

        return ObjDelete;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="evento"></param>
    public static void enviaEmailAtualizacao(Evento evento)
    {
        foreach (EventoAlerta alerta in evento.EventoAlertas)
        {
            Usuario usuario = new UsuarioBLL().CarregarUsuario(new Usuario { UsuarioId = alerta.Usuario.UsuarioId });
            //Ag2.Manager.Entity.ag2mngUser user = new Ag2.Manager.Entity.ag2mngUser();
            Ag2.Net.Mail.MailMessage msg = new Ag2.Net.Mail.MailMessage();
            msg.To.Add(new System.Net.Mail.MailAddress(usuario.EmailUsuario, usuario.NomeUsuario));
            msg.IsBodyHtml = true;
            msg.Subject = string.Concat("Grupo A - Cancelamento de evento");
            msg.PathTemplate = System.Web.HttpContext.Current.Server.MapPath("~/templateMail/AlteracaoAlertaEvento.htm");
            msg.Dictionary.Add("#usuario#", usuario.NomeUsuario);
            msg.Dictionary.Add("#nomeEvento#", evento.ConteudoImprensa.Titulo);
            msg.Dictionary.Add("#status#", (evento.ConteudoImprensa.Ativo ? "Sim" : "N�o"));
            msg.Dictionary.Add("#dataInicio#", evento.DataEventoInicio.ToString());
            msg.Dictionary.Add("#dataFim#", evento.DataEventoFim.ToString());
            msg.Dictionary.Add("#local#", evento.Local);
            new Ag2.Net.Mail.SendMail(msg, false);
        }
    }
}
