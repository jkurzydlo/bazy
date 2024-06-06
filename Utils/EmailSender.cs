using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Mailjet.Client.Resources;
using Mailjet.Client.TransactionalEmails;

namespace bazy1.Utils {
	internal class EmailSender {
		public void send(Models.User user) {

            Mailjet.Client.MailjetClient client = new("883ab859b107ab895f12eae661983acb", "b4bbac4a4adb2649a561c780ee7c6757");

            Mailjet.Client.MailjetRequest request = new Mailjet.Client.MailjetRequest
            {
                Resource = Send.Resource
            };

            var email = new TransactionalEmailBuilder().
                WithFrom(new SendContact("afraczek12@interia.pl")).
                WithSubject("Aktywacja konta").
                WithHtmlPart(@$"
<!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Strict//EN"" ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd"">
<html xmlns=""http://www.w3.org/1999/xhtml"">

<head>
  <meta http-equiv=""Content-Type"" content=""text/html; charset=utf-8"">
  <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
  <title>Aktywacja konta</title>
  <!--[if mso]><style type=""text/css"">body, table, td, a {{ font-family: Arial, Helvetica, sans-serif !important; }}</style><![endif]-->
</head>

<body style=""font-family: Helvetica, Arial, sans-serif; margin: 0px; padding: 0px; background-color: #ffffff;"">
  <table role=""presentation""
    style=""width: 100%; border-collapse: collapse; border: 0px; border-spacing: 0px; font-family: Arial, Helvetica, sans-serif; background-color: rgb(239, 239, 239);"">
    <tbody>
      <tr>
        <td align=""center"" style=""padding: 1rem 2rem; vertical-align: top; width: 100%;"">
          <table role=""presentation"" style=""max-width: 600px; border-collapse: collapse; border: 0px; border-spacing: 0px; text-align: left;"">
            <tbody>
              <tr>
                <td style=""padding: 40px 0px 0px;"">
                  <div style=""text-align: center;"">
                    <div style=""padding-bottom: 20px;""><img src=""https://cdn1.iconfinder.com/data/icons/health-and-medical-glyphs-4/128/162-512.png"" alt=""Company"" style=""width: 128px;""></div>
                  </div>
                  <h3 style=""text-align: center;"">
                    Oprogramowanie dla przychodni Medikat
                  </h3>
                  <div style=""padding: 20px; background-color: rgb(255, 255, 255);"">
                    <div style=""color: rgb(0, 0, 0); text-align: center;"">
                      <h1 style=""margin: 1rem 0"">Aktywacja konta</h1>
                      <p style=""padding-bottom: 16px"">Twój login: {user.Login}</p>

                      <p style=""padding-bottom: 16px"">Kliknij link poniżej aby zweryfikować konto.</p>
                      <p style=""padding-bottom: 16px""><a href=""https://localhost:7137/EmailAuth/EmailConfirm/{user.Token}"" target=""_blank""
                          style=""padding: 12px 24px; border-radius: 4px; color: #FFF; background: #2B52F5;display: inline-block;margin: 0.5rem 0;"">Aktywuj konto</a></p>
                      <p style=""padding-bottom: 16px"">Jeśli ta wiadomość nie jest skierowana do ciebie, zignoruj ją.</p>
                      <p style=""padding-bottom: 16px"">Pozdrawiamy,<br>Zespół Medikat</p>
                    </div>
                  </div>
                </td>
              </tr>
            </tbody>
          </table>
        </td>
      </tr>
    </tbody>
  </table>
</body>

</html>").WithTo(new SendContact(user.Email)).Build();
            var response = client.SendTransactionalEmailAsync(email);
		}
	}
}
