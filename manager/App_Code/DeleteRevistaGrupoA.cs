using System;
using Ag2.Manager.Helper;
using Ag2.Manager.Module;
using Ag2.Manager.Module.Info;
using GrupoA.BusinessLogicalLayer;
using GrupoA.BusinessObject;

/// <summary>
/// Summary description for DeleteUsuario
/// </summary>
public class DeleteRevistaGrupoA
{

    public DeleteRevistaGrupoA()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    /// <summary>
    /// Método configurado no Web.Config para ser executado quando o usuário executa a ação de delete no manager
    /// </summary>
    /// <param name="module"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    public static DeleteRegisterInfo BeforeRegisterDelete(ManagerModuleStructure module, string id)
    {
        DeleteRegisterInfo ObjDelete = new DeleteRegisterInfo();
        //SETADO COMO FALSE PARA ELE NÃO EXECUTAR O DELETE PADRAO DO MANAGER
        ObjDelete.CanDelete = false;
        RevistaGrupoABLL revistaBLL = new RevistaGrupoABLL();
        revistaBLL.ExcluirRevistaGrupoAEdicao(new RevistaGrupoAEdicao() { RevistaGrupoAEdicaoId = Convert.ToInt32(id) });
        Util.ShowMessage("Revista Grupo A excluída com sucesso!", Ag2.Manager.Enumerator.typeMessage.Sucesso);

        //EventoBLL eventoBLL = new EventoBLL();
        //Evento evento = new Evento();
        //evento.EventoId = Convert.ToInt32(id);
        //evento = eventoBLL.CarregarEventoComDependencias(evento);
        //bool exclusaoFisica = true;
        //String nomeArquivo = "";
        //using (TransactionScope scope = new TransactionScope())
        //{
            
        //    eventoBLL.excluirEvento(evento);
        //    // Envia e-mail para usuários avisando sobre o cancelamento
        //    enviaEmailAtualizacao(evento);
        //    // Caso o evento tenha alertas, não deve excluir fisicamente as imagens
        //    if (evento.EventoAlertas.Count == 0)
        //    {
        //        if (evento.EventoImagens != null)
        //        {
        //            foreach (EventoImagem eventoImagem in evento.EventoImagens)
        //            {
        //                nomeArquivo = eventoImagem.Arquivo.NomeArquivoOriginal;
        //                string pathFile = string.Concat(System.Web.HttpContext.Current.Server.MapPath(ConfigurationSettings.AppSettings["uploadRoot"].ToString()), "imagensEvento\\", nomeArquivo);
        //                FileInfo info = new FileInfo(pathFile);
        //                if (info.Exists)
        //                    info.Delete();
        //                else
        //                    exclusaoFisica = false;
        //            }
        //        }
        //        if (exclusaoFisica)
        //        {
        //            scope.Complete();
        //            Util.ShowMessage("Evento excluído com sucesso!", Ag2.Manager.Enumerator.typeMessage.Sucesso);
        //        }
        //        else
        //            throw new FileNotFoundException("Não foi possível encontrar o arquivo físico para exclusão");
        //    }
        //    else
        //    {
        //        //TODO: Deve enviar e-mails para quem tem alertas relacionados à estes eventos
        //        Util.ShowMessage("Evento atualizado com sucesso!<BR><BR>Atenção: em função da existência de alertas, o Evento foi inativado.", Ag2.Manager.Enumerator.typeMessage.Sucesso);
        //    }
        //}

        return ObjDelete;
    }

    public static void enviaEmailAtualizacao(Evento evento)
    {
        evento = new EventoBLL().CarregarEventoComDependencias(evento);
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
			//msg.Dictionary.Add("#nomeEvento#", evento.NomeEvento);
			//msg.Dictionary.Add("#status#", (evento.Ativo ? "Sim" : "Não"));
            msg.Dictionary.Add("#dataInicio#", evento.DataEventoInicio.ToString());
            msg.Dictionary.Add("#dataFim#", evento.DataEventoFim.ToString());
            msg.Dictionary.Add("#local#", evento.Local);
            new Ag2.Net.Mail.SendMail(msg, false);

        }
    }
}
