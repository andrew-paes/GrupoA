using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using GrupoA.BusinessObject;
using System.Net.Mail;
using System.Web;
using System.IO;
using GrupoA.DataAccess.ADO;
using GrupoA.BusinessObject.Enumerator;


namespace GrupoA.BusinessLogicalLayer
{
    public class ContatoBLL : BaseBLL
    {

        public ContatoBLL()
        {

        }

        /// <summary>
        /// Envia email do contato de Fale Conosco
        /// </summary>
        public void EnviaFaleConosco(ContatoFaleConosco contato)
        {
            //Carrega Dados relacionados
            ContatoSetor setor = new ContatoSetor();
            setor = new ContatoSetorADO().Carregar(contato.Setor);
            contato.Setor = setor;
            ContatoAssunto assunto = new ContatoAssunto();
            assunto = new ContatoAssuntoADO().Carregar(contato.Assunto);
            contato.Assunto = assunto;

            // Título do e-mail
            string tituloEmail = "Grupo A - Contato";
            // E-mail de origem
            //string emailOrigem = Convert.ToString(ConfigurationSettings.AppSettings["emailSite"]);
            string emailOrigem = contato.Email;


            // Cabeçalho do e-mail
            MailMessage msg = new MailMessage();
            msg.Body = getTemplateEmail(contato);
            msg.IsBodyHtml = true;
            msg.Subject = tituloEmail;
            msg.From = new System.Net.Mail.MailAddress(emailOrigem);
            SmtpClient smtp = new SmtpClient(ConfigurationManager.AppSettings["smtpServer"].ToString());

            foreach (ContatoResponsavel responsavel in new ContatoResponsavelADO().Carregar(contato.Assunto))
            {
                msg.To.Add(new System.Net.Mail.MailAddress(responsavel.EmailResonsavel));

                // Envia e-mail
                smtp.Send(msg);
            }
        }

        public void EnviaIndique(ContatoIndique contato)
        {


            // Título do e-mail
            string tituloEmail = "Artmed - Indicação de " + contato.SeuNome;
            // E-mail de origem
            //string emailOrigem = Convert.ToString(ConfigurationSettings.AppSettings["emailSite"]);
            string emailOrigem = contato.SeuEmail;

            // Cabeçalho do e-mail
            MailMessage msg = new MailMessage();
            getTemplateEmail(contato);
            msg.IsBodyHtml = true;
            msg.Subject = tituloEmail;
            msg.From = new System.Net.Mail.MailAddress(emailOrigem);

            SmtpClient smtp = new SmtpClient(ConfigurationSettings.AppSettings["smtpServer"].ToString());


            msg.To.Add(new System.Net.Mail.MailAddress(contato.EmailAmigo));

            // Envia e-mail
            smtp.Send(msg);

        }

        private string getTemplateEmail(ContatoIndique contato)
        {
            StreamReader reader = new StreamReader(contato.TemplatePath, Encoding.GetEncoding("ISO-8859-1"));
            string htmlTamplate = reader.ReadToEnd();
            string retorno = "";

            if (htmlTamplate != null)
            {
                //line = reader.ReadLine();
                htmlTamplate = htmlTamplate.Replace("#SeuNome#", contato.SeuNome);
                htmlTamplate = htmlTamplate.Replace("#SeuEmail#", contato.SeuEmail);
                htmlTamplate = htmlTamplate.Replace("#EmailAmigo#", contato.EmailAmigo);
                htmlTamplate = htmlTamplate.Replace("#NomeAmigo#", contato.NomeAmigo);
                htmlTamplate = htmlTamplate.Replace("#Observacao#", contato.Observacao);
                htmlTamplate = htmlTamplate.Replace("#url#", contato.Url);
                htmlTamplate = htmlTamplate.Replace("#CaminhoSite#", ConfigurationManager.AppSettings["CaminhoImagem"].ToString());

                reader.Dispose();
                retorno = htmlTamplate.ToString();
            }

            //return htmlTamplate.ToString();
            return retorno;
        }

        private string getTemplateEmail(ContatoFaleConosco contato)
        {
            StreamReader reader = new StreamReader(contato.TemplatePath, Encoding.GetEncoding("ISO-8859-1"));
            string htmlTamplate = reader.ReadToEnd();
            string retorno = "";

            if (htmlTamplate != null)
            {
                //line = reader.ReadLine();
                htmlTamplate = htmlTamplate.Replace("#Titulo#", "Sobre o GrupoA - Conosco");
                htmlTamplate = htmlTamplate.Replace("#Nome#", contato.Nome);
                htmlTamplate = htmlTamplate.Replace("#Assunto#", contato.Assunto.NomeAssunto.ToString());
                htmlTamplate = htmlTamplate.Replace("#Setor#", contato.Setor.NomeSetor.ToString());
                htmlTamplate = htmlTamplate.Replace("#Mensagem#", contato.Mensagem.ToString());
                htmlTamplate = htmlTamplate.Replace("#ReceberInformacoes#", contato.Optin ? "Sim" : "Não");
                htmlTamplate = htmlTamplate.Replace("#CaminhoSite#", ConfigurationManager.AppSettings["CaminhoImagem"].ToString());

                reader.Dispose();
                retorno = htmlTamplate.ToString();
            }

            //return htmlTamplate.ToString();
            return retorno;
        }

        private string getTemplateEmailRevista(ContatoFaleConosco contato, String tituloEmail)
        {
            StreamReader reader = new StreamReader(contato.TemplatePath, Encoding.GetEncoding("ISO-8859-1"));
            string htmlTamplate = reader.ReadToEnd();
            string retorno = "";

            if (htmlTamplate != null)
            {
                //line = reader.ReadLine();
                htmlTamplate = htmlTamplate.Replace("#Revista#", tituloEmail);
                htmlTamplate = htmlTamplate.Replace("#Nome#", contato.Nome);
                htmlTamplate = htmlTamplate.Replace("#Estado#", contato.Municipio.Regiao.NomeRegiao);
                htmlTamplate = htmlTamplate.Replace("#Cidade#", contato.Municipio.NomeMunicipio);
                htmlTamplate = htmlTamplate.Replace("#Telefone#", String.Concat(contato.Telefone.DddTelefone, " - ", contato.Telefone.NumeroTelefone));
                htmlTamplate = htmlTamplate.Replace("#Assunto#", contato.Assunto.NomeAssunto.ToString());
                htmlTamplate = htmlTamplate.Replace("#Setor#", contato.Setor.NomeSetor.ToString());
                htmlTamplate = htmlTamplate.Replace("#Mensagem#", contato.Mensagem.ToString());
                htmlTamplate = htmlTamplate.Replace("#ReceberInformacoes#", contato.Optin ? "Sim" : "Não");
                htmlTamplate = htmlTamplate.Replace("#CaminhoSite#", ConfigurationManager.AppSettings["CaminhoImagem"].ToString());

                reader.Dispose();
                retorno = htmlTamplate.ToString();
            }

            //return htmlTamplate.ToString();
            return retorno;
        }

        public void EnviaFaleConoscoRevista(ContatoFaleConosco contato, AreaDeRevista areaRevista)
        {
            //Carrega Dados relacionados
            ContatoSetor setor = new ContatoSetor();
            setor = new ContatoSetorADO().Carregar(contato.Setor);
            contato.Setor = setor;
            ContatoAssunto assunto = new ContatoAssunto();
            assunto = new ContatoAssuntoADO().Carregar(contato.Assunto);
            contato.Assunto = assunto;
            contato.Municipio = new MunicipioADO().Carregar(contato.Municipio);
            contato.Municipio.Regiao = new RegiaoADO().Carregar(contato.Municipio.Regiao);

            string tituloEmail = null;
            if (areaRevista == AreaDeRevista.Bmj)
            {
                tituloEmail = String.Concat("Grupo A | Revista BMJ - Fale Conosco");
            }
            else 
            {
                tituloEmail = String.Concat("Grupo A | Revista Pátio - Fale Conosco");
            }

            // E-mail de origem
            string emailOrigem = contato.Email;

            // Cabeçalho do e-mail
            MailMessage msg = new MailMessage();
            msg.Body = getTemplateEmailRevista(contato, tituloEmail);
            msg.IsBodyHtml = true;
            msg.Subject = tituloEmail;
            msg.From = new System.Net.Mail.MailAddress(GrupoA.GlobalResources.GrupoA_Resource.EmailSAC);
            msg.ReplyTo = new System.Net.Mail.MailAddress(emailOrigem);
            SmtpClient smtp = new SmtpClient(ConfigurationManager.AppSettings["smtpServer"].ToString());

            foreach (ContatoResponsavel responsavel in new ContatoResponsavelADO().Carregar(contato.Assunto))
            {
                msg.To.Add(new System.Net.Mail.MailAddress(responsavel.EmailResonsavel));

                // Envia e-mail
                smtp.Send(msg);
            }
        }
    }
}
