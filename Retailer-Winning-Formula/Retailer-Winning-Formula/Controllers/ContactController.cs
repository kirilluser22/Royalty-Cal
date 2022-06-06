using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Retailer_Winning_Formula.Infrastructure.Options;
using Retailer_Winning_Formula.Infrastructure.Services.EmailService;
using Retailer_Winning_Formula.Models;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Retailer_Winning_Formula.Controllers
{
    public class ContactController : Controller
    {
        private readonly IEmailkitService _emailService;
        private readonly ILogger<ContactController> _logger;
        private readonly CompanyEmailsOption _companyEmail;
        public ContactController(ILogger<ContactController> logger,
             IEmailkitService emailService,
             IOptions<CompanyEmailsOption> companyEmail)
        {
            _logger = logger;
            _emailService = emailService;
            _companyEmail = companyEmail.Value;
        }
        public async Task<object> SendContactEmail(ContactFormRequestModel request)
        {
            var recepients = new List<string>();
            if (!string.IsNullOrWhiteSpace(_companyEmail.SalesAdmin))
                recepients.Add(_companyEmail.SalesAdmin);
            recepients.Add(request.ContactEmail);
            var mailBody = BodyGenerator(request);
            _emailService.SendAsync(recepients, null, null, "Request for More Info is Received", mailBody, null);
            return Ok("Sent Successfully");
        }
        private static string BodyGenerator(ContactFormRequestModel userInfo)
        {
            var mailBody = new StringBuilder();
            mailBody.Append("<div style='margin-bottom:20px'>From Retailer Portal</div>");
            mailBody.AppendFormat($"<table>");
            mailBody.AppendFormat($"<tr><td>First Name: </td><td style='padding-left:20px'>{userInfo.ContactFirstName}</td></tr>");
            mailBody.AppendFormat($"<tr><td>Last Name: </td><td style='padding-left:20px'>{userInfo.ContactLastName}</td></tr>");
            if (!string.IsNullOrWhiteSpace(userInfo.ContactStore))
                mailBody.AppendFormat($"<tr><td>Store: </td><td style='padding-left:20px'>{userInfo.ContactStore}</td></tr>");
            if (!string.IsNullOrWhiteSpace(userInfo.ContactLocation))
                mailBody.AppendFormat($"<tr><td>Location: </td><td style='padding-left:20px'>{userInfo.ContactLocation}</td></tr>");
            mailBody.AppendFormat($"<tr><td>Email: </td><td style='padding-left:20px'>{userInfo.ContactEmail}</td></tr>");
            if (userInfo.ContactPhoneNumber != null && userInfo.ContactPhoneNumber != 0)
                mailBody.AppendFormat($"<tr><td>PhoneNumber: </td><td style='padding-left:20px'>{userInfo.ContactPhoneNumber}</td></tr>");
            if (!string.IsNullOrWhiteSpace(userInfo.ContactRepName))
                mailBody.AppendFormat($"<tr><td>Your ZucoraHome Rep's Name:</td><td style='padding-left:20px'>{userInfo.ContactRepName}</td></tr>");
            mailBody.AppendFormat($"<tr><td>I don’t know who my Rep is.:</td><td style='padding-left:20px'>{(userInfo.WhoMyRep ? "Yes" : "No")}</td></tr>");
            mailBody.AppendFormat($"</table>");
            return mailBody.ToString();
        }
    }
}
