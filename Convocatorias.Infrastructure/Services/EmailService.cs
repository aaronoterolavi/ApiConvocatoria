using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Convocatorias.Application.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Convocatorias.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;
        public EmailService(IConfiguration config) => _config = config;

        public async Task SendResetPasswordEmailAsync(string toEmail, string displayName, string resetUrl)
        {
            var s = _config.GetSection("EmailSettings");
            var smtp = s["SmtpServer"]!;
            var port = int.Parse(s["Port"] ?? "587");
            var enableSsl = bool.Parse(s["EnableSsl"] ?? "true");
            var user = s["User"]!;
            var pass = s["Password"]!;
            var from = s["From"]!;
            var display = s["DisplayName"] ?? "Sistema de Convocatorias";

            var body = GetHtmlTemplate(displayName, resetUrl);

            using var message = new MailMessage();
            message.From = new MailAddress(from, display);
            message.To.Add(toEmail);
            message.Subject = "Restablecimiento de contraseña";
            message.Body = body;
            message.IsBodyHtml = true;

            using var client = new SmtpClient(smtp, port)
            {
                Credentials = new NetworkCredential(user, pass),
                EnableSsl = enableSsl
            };

            await client.SendMailAsync(message);
        }

        private string GetHtmlTemplate(string nombre, string enlace)
        {
            // Usa el HTML que me diste, reemplaza [Enlace de Restablecimiento] y [Nombre del usuario]
            var html = @"<!DOCTYPE html>
<html lang=""es"">

<head>
  <meta charset=""UTF-8"" />
  <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"" />
  <title>Restablecimiento de Contraseña</title>
  <style>
    body {
      margin: 0;
      padding: 0;
      font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
      background-color: #f4f6f8;
      color: #333333;
    }

    a {
      color: #1a73e8;
      text-decoration: none;
    }

    a:hover {
      text-decoration: underline;
    }

    .container {
      max-width: 600px;
      margin: 30px auto;
      background-color: #ffffff;
      border-radius: 8px;
      box-shadow: 0 4px 12px rgba(0,0,0,0.1);
      overflow: hidden;
      border: 1px solid #ddd;
    }

    .header {
      background-color: #28a745;  
      padding: 20px;
      text-align: center;
    }

    .header img {
      max-height: 50px;
    }

    .body {
      padding: 30px 40px;
    }

    .body h1 {
      color: #28a745;
      font-size: 28px;
      margin-bottom: 10px;
    }

    .body p {
      font-size: 16px;
      line-height: 1.5;
      margin-bottom: 20px;
    }

    .btn-reset {
      display: inline-block;
      background-color: #28a745;
      color: white !important;
      font-weight: 600;
      text-align: center;
      padding: 14px 28px;
      border-radius: 6px;
      font-size: 16px;
      margin: 20px 0;
      cursor: pointer;
    }

    .footer {
      background-color: #f0f0f0;
      text-align: center;
      font-size: 13px;
      color: #777;
      padding: 15px 20px;
      border-top: 1px solid #ddd;
    }

    @media only screen and (max-width: 620px) {
      .container {
        width: 95% !important;
        margin: 10px auto;
      }

      .body {
        padding: 20px 15px;
      }

      .body h1 {
        font-size: 24px;
      }

      .btn-reset {
        width: 100%;
        padding: 14px 0;
      }
    }
  </style>
</head>

<body>
  <div class=""container"" role=""main"">
    <div class=""header"">
      <img src=""https://intranet.agrorural.gob.pe/mesadepartes/Images/agrorural.png"" alt=""Logo AgroRural"" />
    </div>
    <div class=""body"">
      <h1>Restablece tu contraseña</h1>
      <p>Hola 
        <!-- <strong>[Nombre del usuario]</strong> -->
        ,</p>
      <p>Hemos recibido una solicitud para restablecer la contraseña de tu cuenta en el <strong>Sistema de Convocatorias</strong>.</p>
      <p>Para continuar, haz clic en el botón de abajo y sigue las instrucciones:</p>
      <p style=""text-align:center;"">
        <a href=""[Enlace de Restablecimiento]"" class=""btn-reset"" target=""_blank"" rel=""noopener"">Restablecer contraseña</a>
      </p>
      <p>Si no solicitaste este cambio, puedes ignorar este mensaje y tu contraseña seguirá igual.</p>
      <p>Saludos,<br />
        <strong>El equipo de Sistemas de Convocatoria</strong></p>
      <p>
        <a href=""https://www.gob.pe/agrorural"" target=""_blank"" rel=""noopener"">Programa de Desarrollo Productivo Agrario Rural</a>
      </p>
    </div>
    <div class=""footer"">
      <p>© 2025 Programa de Desarrollo Productivo Agrario Rural - Todos los derechos reservados</p>
    </div>
  </div>
</body>

</html>
"; // pega aquí el HTML completo que me diste
            html = html.Replace("[Enlace de Restablecimiento]", enlace)
                       .Replace("[Nombre del usuario]", nombre ?? "");
            return html;
        }
    }
}
