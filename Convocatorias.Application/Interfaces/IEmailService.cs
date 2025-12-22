using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Convocatorias.Application.Interfaces
{
    public interface IEmailService
    {
        Task SendResetPasswordEmailAsync(string toEmail, string displayName, string resetUrl);

        Task SendUserCreatedEmailAsync(
       string toEmail,
       string displayName,
       string usuario,
       string loginUrl
   );
    }

}
