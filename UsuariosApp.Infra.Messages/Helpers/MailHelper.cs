using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using UsuariosApp.Domain.Models;

namespace UsuariosApp.Infra.Messages.Helpers
{
    public class MailHelper
    {
        #region Atributos

        private static string _conta = "sergiojavaarq@outlook.com";
        private static string _senha = "@Admin12345";
        private static string _smtp = "smtp-mail.outlook.com";
        private static int _porta = 587;

        #endregion

        #region Método para envio de email

        public static void SendMail(Mensagem mensagem)
        {
            #region Criar o email

            var mailMessage = new MailMessage(_conta, mensagem.Destinatario);
            mailMessage.Subject = mensagem.Assunto; 
            mailMessage.Body = mensagem.Texto;

            #endregion

            #region Enviar o email

            var smtpClient = new SmtpClient(_smtp, _porta); 
            smtpClient.Credentials = new NetworkCredential(_conta, _senha); 
            smtpClient.EnableSsl = true; 
            smtpClient.Send(mailMessage); 

            #endregion


            
        }
        #endregion

    }
}
