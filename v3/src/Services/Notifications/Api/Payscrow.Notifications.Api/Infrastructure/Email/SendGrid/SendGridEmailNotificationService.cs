using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Payscrow.Notifications.Api.Application.Enumerations;
using Payscrow.Notifications.Api.Application.Interfaces;
using Payscrow.Notifications.Api.Application.Models;
using Payscrow.Notifications.Api.Interfaces;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Payscrow.Notifications.Api.Infrastructure.SendGrid
{
    public class SendGridEmailNotificationService : IEmailNotificationService
    {
        private readonly ILogger _logger;
        private readonly INotificationDbContext _context;

        private Tenant _tenant;
        private EmailProvider _emailProvider;
        private EmailTemplate _emailTemplate;

        public SendGridEmailNotificationService(ILogger<SendGridEmailNotificationService> logger, INotificationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<string> GetEmailContent(Guid tenantId, EmailMessageType emailMessageType, Dictionary<string, object> values)
        {
            var emailProvider = await GetEmailProviderAsync(EmailProviderType.SendGrid, tenantId);

            if (emailProvider is null)
            {
                _logger.LogWarning("---- Could not get Email Content because Provider Credentials could not be found in the database for Tenant with ID: {0} ----", tenantId);
                return string.Empty;
            }

            var client = new SendGridClient(emailProvider.ProviderApiKey?.Trim());

            var template_id = await GetTemplateIdAsync(emailMessageType, tenantId);

            var response = await client.RequestAsync(
                method: BaseClient.Method.GET,
                urlPath: $"templates/{template_id}"
            );

            if (response.IsSuccessStatusCode)
            {
                return await response.Body.ReadAsStringAsync();
            }
            else
            {
                _logger.LogWarning("---- Sendgrid template could not be retrived with response code {Code} -----", response.StatusCode);
                return "";
            }
        }

        public async Task SendAsync(Guid tenantId, string to, string subject, string message)
        {
            var tenant = await GetTenantAsync(tenantId);

            if (tenant == null)
            {
                _logger.LogWarning("--- Could not send email because Tenant could not be resolved from the ID: {TenantID} ----", tenantId);
                return;
            }

            var emailProvider = await GetEmailProviderAsync(EmailProviderType.SendGrid, tenantId);

            if (emailProvider is null)
            {
                _logger.LogWarning("---- Could not Send Email because Provider Credentials could not be found in the database for Tenant with ID: {TenantID} ----", tenantId);
                return;
            }

            var client = new SendGridClient(emailProvider.ProviderApiKey?.Trim());

            var from = new EmailAddress(tenant.EmailAddress, tenant.Name);
            var toEmail = new EmailAddress(to);

            const string pattern = @"</?\w+((\s+\w+(\s*=\s*(?:"".*?""|'.*?'|[^'"">\s]+))?)+\s*|\s*)/?>";
            var plainTextBody = Regex.Replace(message, pattern, string.Empty);

            var msg = MailHelper.CreateSingleEmail(from, toEmail, subject, plainTextBody, message);
            var response = await client.SendEmailAsync(msg);

            if (!response.IsSuccessStatusCode)
            {
                var requestBody = await response.Body.ReadAsStringAsync();
                _logger.LogError("---- Could not send Email from SendGrid with the following response. {NewLine} {RequestBody} ----", Environment.NewLine, requestBody);
            }
        }

        public async Task SendAsync(Guid tenantId, EmailMessageType emailMessageType, string to, string subject, Dictionary<string, object> values)
        {
            var tenant = await GetTenantAsync(tenantId);

            if (tenant == null)
            {
                _logger.LogWarning("--- Could not send email because Tenant could not be resolved from the ID: {TenantID} ----", tenantId);
                return;
            }

            var emailProvider = await GetEmailProviderAsync(EmailProviderType.SendGrid, tenantId);

            if (emailProvider is null)
            {
                _logger.LogWarning("---- Could not Send Email because Provider Credentials could not be found in the database for Tenant with ID: {TenantID} ----", tenantId);
                return;
            }

            var client = new SendGridClient(emailProvider.ProviderApiKey?.Trim());

            var from = new EmailAddress(tenant.EmailAddress, tenant.Name);

            var toEmail = new EmailAddress(to);
            var adminCopy = new EmailAddress(tenant.EmailAddress);

            var templateId = await GetTemplateIdAsync(emailMessageType, tenantId);

            if (string.IsNullOrWhiteSpace(templateId))
            {
                _logger.LogWarning("---- Could not Send Email because Provider TEMPLATE ID could not be found in the database for Tenant with ID: {TenantID} ----", tenantId);
                return;
            }

            var msg = MailHelper.CreateSingleTemplateEmailToMultipleRecipients(from, new List<EmailAddress> { toEmail, adminCopy }, templateId, values);

            _logger.LogInformation("---- Sending Email Message With SendGrid To: {To}, with Subject: {Subject} -----", to, subject);

            var response = await client.SendEmailAsync(msg);

            if (!response.IsSuccessStatusCode)
            {
                var requestBody = await response.Body.ReadAsStringAsync();
                _logger.LogError("---- Could not send Email from SendGrid with the following response. {NewLine} {RequestBody} ----", Environment.NewLine, requestBody);
            }
        }

        private async Task<Tenant> GetTenantAsync(Guid tenantId)
        {
            if (_tenant != null && _tenant.Id == tenantId) return _tenant;

            _tenant = await _context.Tenants.FirstOrDefaultAsync(x => x.Id == tenantId);

            return _tenant;
        }

        private async Task<string> GetTemplateIdAsync(EmailMessageType type, Guid tenantId)
        {
            var emailTemplate = _emailTemplate != null && _emailTemplate.TenantId == tenantId
                ? _emailTemplate
                : await (from p in _context.EmailProviders
                         join t in _context.EmailTemplates on p.Id equals t.EmailProviderId
                         where p.Type == EmailProviderType.SendGrid && p.TenantId == tenantId && t.MessageType == type
                         select t).FirstOrDefaultAsync();

            _emailTemplate = emailTemplate;

            return emailTemplate?.ProviderTemplateId?.Trim();
        }

        private async Task<EmailProvider> GetEmailProviderAsync(EmailProviderType type, Guid tenantId)
        {
            if (_emailProvider != null && _emailProvider.TenantId == tenantId) return _emailProvider;

            _emailProvider = await _context.EmailProviders.FirstOrDefaultAsync(x => x.Type == type && x.TenantId == tenantId);

            return _emailProvider;
        }
    }
}