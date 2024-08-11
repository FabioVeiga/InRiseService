
using InRiseService.Application.DTOs.ApiSettingDto;
using InRiseService.Application.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace InRiseService.Application.Services
{
    public class SendGridService : ISendGridService
    {
        private readonly SendGridSetting _sendGridSetting;
        private readonly ILogger<SendGridService> _logger;
        private readonly KeyVaultService _keyVaultService;

        public SendGridService(
            IOptions<SendGridSetting> options,
            ILogger<SendGridService> logger,
            KeyVaultService keyVaultService
            ) 
        {
            _sendGridSetting = options.Value;
            _keyVaultService = keyVaultService;
            _logger = logger;
            GetApiKey().GetAwaiter();
        }

        private async Task GetApiKey(){
            _sendGridSetting.ApiKey = await _keyVaultService.GetSecretAsync("sendgrid");
        }

        public async Task<bool> SendAsync(string toEmail, string name, string subject, string message)
        {
            try
            {
               
                var msg = new SendGridMessage()
                {
                    Personalizations = new List<Personalization>()
                {
                    new()
                    {
                        Tos = new List<EmailAddress>()
                        {
                            new()
                            {
                                Email = toEmail,
                                Name = name
                            }
                        },
                    }
                },
                From = new EmailAddress()
                {
                    Email = _sendGridSetting.FromEmail,
                    Name = _sendGridSetting.Name
                },
                Subject = subject,
                Contents = new List<Content>()
                {
                    new()
                    {
                        Type = "text/plain",
                        Value = message
                    }
                }
                };
                var client = new SendGridClient(_sendGridSetting.ApiKey);
                var response = await client.SendEmailAsync(msg);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to send email: {Ex}", ex);
                throw;
            }
        }

        public async Task<bool> SendByTemplateAsync(string toEmail, string subject, IDictionary<string, string> substitutions, string templateId)
        {
            try
            {
                var client = new SendGridClient(_sendGridSetting.ApiKey);
                var msg = new SendGridMessage();
                msg.SetFrom(new EmailAddress(_sendGridSetting.FromEmail, _sendGridSetting.Name));
                msg.AddTo(new EmailAddress(toEmail));
                msg.SetTemplateId(templateId);
                msg.SetSubject(subject);
                msg.SetTemplateData(substitutions);
                var response = await client.SendEmailAsync(msg);
                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}