using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UJAS.Core.Entities;
using UJAS.Core.Entities.Company;
using UJAS.Core.Entities.Field;
using UJAS.Core.Entities.System;
using UJAS.Core.Entities.User;
using UJAS.Core.Enums;
using UJAS.Infrastructure.Data;

namespace UJAS.Infrastructure.Seed
{
    public interface IDataSeeder
    {
        Task SeedAsync();
    }

    public class DataSeeder : IDataSeeder
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<tUser> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public DataSeeder(
            ApplicationDbContext context,
            UserManager<tUser> userManager,
            RoleManager<Role> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task SeedAsync()
        {
            await _context.Database.MigrateAsync();

            await SeedRolesAsync();
            await SeedSystemFieldsAsync();
            await SeedSystemSettingsAsync();
            await SeedEmailTemplatesAsync();
            await SeedDefaultCompanyAsync();
            await SeedAdminUserAsync();

            await _context.SaveChangesAsync();
        }

        private async Task SeedRolesAsync()
        {
            var roles = new[]
            {
                new Role { Name = "SystemAdministrator", NormalizedName = "SYSTEMADMINISTRATOR", Description = "Global system administrator" },
                new Role { Name = "CompanyAdministrator", NormalizedName = "COMPANYADMINISTRATOR", Description = "Company-level administrator" },
                new Role { Name = "RegionalManager", NormalizedName = "REGIONALMANAGER", Description = "Manages multiple locations" },
                new Role { Name = "Manager", NormalizedName = "MANAGER", Description = "Manages a single location" },
                new Role { Name = "Applicant", NormalizedName = "APPLICANT", Description = "Job applicant" }
            };

            foreach (var role in roles)
            {
                if (!await _roleManager.RoleExistsAsync(role.Name))
                {
                    await _roleManager.CreateAsync(role);
                }
            }
        }

        private async Task SeedSystemFieldsAsync()
        {
            if (await _context.SystemFields.AnyAsync())
                return;

            var systemFields = new List<SystemField>
            {
                // Personal Information
                new SystemField
                {
                    Name = "Salutation",
                    Label = "Salutation",
                    FieldType = FieldType.Dropdown,
                    FieldCategory = FieldCategory.PersonalInformation,
                    Section = "Personal Information",
                    DisplayOrder = 1,
                    IsRequired = false,
                    IsDefault = true,
                    IsEditableByCompany = true,
                    CanBeHiddenByCompany = true,
                    PrivacyLevel = PrivacyLevel.PII,
                    Options = @"[""Mr."",""Mrs."",""Ms."",""Mx."",""Dr."",""Prof."",""Other""]"
                },
                new SystemField
                {
                    Name = "FirstName",
                    Label = "First Name (Legal)",
                    FieldType = FieldType.Text,
                    FieldCategory = FieldCategory.PersonalInformation,
                    Section = "Personal Information",
                    DisplayOrder = 2,
                    IsRequired = true,
                    IsDefault = true,
                    IsEditableByCompany = false,
                    CanBeHiddenByCompany = false,
                    PrivacyLevel = PrivacyLevel.PII,
                    ValidationRules = @"{""MinLength"":1,""MaxLength"":100,""Pattern"":""^[a-zA-Z\\s\\-\\.']+$""}"
                },
                // Add all other fields from specification...
            };

            await _context.SystemFields.AddRangeAsync(systemFields);
        }

        private async Task SeedSystemSettingsAsync()
        {
            if (await _context.SystemSettings.AnyAsync())
                return;

            var settings = new List<SystemSettings>
            {
                new SystemSettings
                {
                    SettingKey = "Application.RetentionDays",
                    SettingValue = "365",
                    Description = "Number of days to retain applications before auto-archiving",
                    Category = "Application",
                    IsEditable = true
                },
                new SystemSettings
                {
                    SettingKey = "Security.PasswordExpiryDays",
                    SettingValue = "90",
                    Description = "Number of days before password expires",
                    Category = "Security",
                    IsEditable = true
                },
                new SystemSettings
                {
                    SettingKey = "Email.FromAddress",
                    SettingValue = "noreply@ujas.com",
                    Description = "Default from email address",
                    Category = "Email",
                    IsEditable = true
                },
                new SystemSettings
                {
                    SettingKey = "FileUpload.MaxSizeMB",
                    SettingValue = "10",
                    Description = "Maximum file upload size in MB",
                    Category = "FileUpload",
                    IsEditable = true
                }
            };

            await _context.SystemSettings.AddRangeAsync(settings);
        }

        private async Task SeedEmailTemplatesAsync()
        {
            if (await _context.EmailTemplates.AnyAsync())
                return;

            var templates = new List<EmailTemplate>
            {
                new EmailTemplate
                {
                    TemplateName = "ApplicationConfirmation",
                    Subject = "Application Confirmation - {{CompanyName}}",
                    Body = @"<!DOCTYPE html>
<html>
<head>
    <style>
        body { font-family: Arial, sans-serif; line-height: 1.6; color: #333; }
        .container { max-width: 600px; margin: 0 auto; padding: 20px; }
        .header { background-color: #007bff; color: white; padding: 20px; text-align: center; }
        .content { padding: 20px; background-color: #f8f9fa; }
        .footer { text-align: center; padding: 20px; font-size: 12px; color: #666; }
    </style>
</head>
<body>
    <div class='container'>
        <div class='header'>
            <h1>Application Received</h1>
        </div>
        <div class='content'>
            <p>Dear {{ApplicantName}},</p>
            <p>Thank you for applying for the {{Position}} position at {{CompanyName}}.</p>
            <p><strong>Application Details:</strong></p>
            <ul>
                <li>Application Number: {{ApplicationNumber}}</li>
                <li>Position: {{Position}}</li>
                <li>Date Submitted: {{SubmissionDate}}</li>
            </ul>
            <p>We have received your application and will review it shortly. If your qualifications match our requirements, we will contact you for the next steps.</p>
            <p>You can check the status of your application by logging into your account.</p>
            <p>Best regards,<br>{{CompanyName}} Hiring Team</p>
        </div>
        <div class='footer'>
            <p>This is an automated message. Please do not reply to this email.</p>
        </div>
    </div>
</body>
</html>",
                    IsHtml = true,
                    Variables = @"[""ApplicantName"",""CompanyName"",""Position"",""ApplicationNumber"",""SubmissionDate""]",
                    Category = "Application",
                    IsSystemTemplate = true
                }
                // Add more templates...
            };

            await _context.EmailTemplates.AddRangeAsync(templates);
        }

        private async Task SeedDefaultCompanyAsync()
        {
            if (await _context.Companies.AnyAsync())
                return;

            var defaultCompany = new tCompany
            {
                Name = "Demo Company",
                LegalName = "Demo Company Inc.",
                Website = "https://demo.company.com",
                Industry = "Technology",
                Description = "Demo company for testing purposes",
                PrimaryColor = "#007bff",
                SecondaryColor = "#6c757d",
                IsActive = true,
                Settings = new CompanySettings
                {
                    ApplicationRetentionDays = 365,
                    AutoReplyToApplicants = true,
                    ShowSalaryFields = true,
                    CollectEEOData = true
                }
            };

            await _context.Companies.AddAsync(defaultCompany);
        }

        private async Task SeedAdminUserAsync()
        {
            if (await _userManager.Users.AnyAsync(u => u.Email == "admin@ujas.com"))
                return;

            var adminUser = new tUser
            {
                FirstName = "System",
                LastName = "Administrator",
                Email = "admin@ujas.com",
                UserName = "admin@ujas.com",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            var result = await _userManager.CreateAsync(adminUser, "Admin@123");
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(adminUser, "SystemAdministrator");
            }
        }
    }
}