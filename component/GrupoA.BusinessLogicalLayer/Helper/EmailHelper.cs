using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Text;

namespace GrupoA.BusinessLogicalLayer.Helper
{
    public class EmailHelper
    {
        public void EnviarEmail(string emitente, string destinatario, string assunto, StringBuilder template)
        {
            this.EnviarEmail(emitente, null, destinatario, assunto, template);
        }

        public void EnviarEmail(string emitente, string emailResposta, string destinatario, string assunto, StringBuilder template)
        {
            this.EnviarEmail(emitente, emailResposta, destinatario, assunto, template, null);
        }

        /// <summary>
        /// Envia email
        /// </summary>
        public void EnviarEmail(string emitente, string emailResposta, string destinatario, string assunto, StringBuilder template, String anexo)
        {
            // Cabeçalho do e-mail
            MailMessage msg = new MailMessage();
            msg.IsBodyHtml = true;
            msg.Body = template.ToString();
            msg.Subject = assunto;
            msg.From = new System.Net.Mail.MailAddress(emitente);
            msg.ReplyTo = new System.Net.Mail.MailAddress(!String.IsNullOrEmpty(emailResposta) ? emailResposta : emitente);

            if (!String.IsNullOrEmpty(anexo))
            {
                msg.Attachments.Add( new Attachment(anexo));
            }

            SmtpClient smtp = new SmtpClient(System.Configuration.ConfigurationManager.AppSettings["smtpServer"].ToString());

            msg.To.Add(new System.Net.Mail.MailAddress(destinatario));
            // Envia o e-mail
            //try
            //{
            smtp.Send(msg);
            //}
            //catch { }

            msg.Dispose();
            smtp.Dispose();
        }

        /// <summary>
        /// Método que popula o template do email que será enviado
        /// </summary>
        /// <param name="dicionarioDados">Dicionário de dados que contem os campos a serem substituidos no template do email</param>
        /// <param name="template">Caminho completo do template do email</param>
        /// <returns></returns>
        public StringBuilder PopulaTemplateEmail(Dictionary<string, string> dicionarioDados, string template)
        {
            StreamReader reader = new StreamReader(template, Encoding.GetEncoding("ISO-8859-1"));
            string htmlTemplate = reader.ReadToEnd();
            StringBuilder retorno = new StringBuilder();

            if (htmlTemplate != null)
            {
                // Percorre dicionário de dados fazendo replace pelos campos do template do email
                foreach (var item in dicionarioDados)
                {
                    htmlTemplate = htmlTemplate.Replace("#" + item.Key + "#", item.Value);
                }

                // Libera o objeto reader
                reader.Dispose();
                retorno.Append(htmlTemplate.ToString());
            }

            return retorno;
        }
    }
}
