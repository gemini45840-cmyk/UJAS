using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Users.DTOs
{
    public class LoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; } = false;
        public string RecaptchaToken { get; set; }
        public string DeviceId { get; set; }
        public string DeviceName { get; set; }
        public string UserAgent { get; set; }
        public string IpAddress { get; set; }
    }

    public class LoginResponseDto
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public DateTime TokenExpiry { get; set; }
        public DateTime RefreshTokenExpiry { get; set; }
        public string UserType { get; set; }
        public List<string> Roles { get; set; } = new();
        public List<string> Permissions { get; set; } = new();
        public Guid? CompanyId { get; set; }
        public string CompanyName { get; set; }
        public List<Guid> LocationIds { get; set; } = new();
        public Guid? ProfileId { get; set; }
        public bool RequiresPasswordChange { get; set; }
        public bool TwoFactorRequired { get; set; }
        public string TwoFactorMethod { get; set; } // Email, Phone, Authenticator
        public Dictionary<string, object> UserPreferences { get; set; } = new();
        public DateTime LastLoginDate { get; set; }
    }

    public class TwoFactorDto
    {
        public string Code { get; set; }
        public string Method { get; set; } // Email, Phone, Authenticator
        public bool RememberDevice { get; set; } = false;
        public string DeviceId { get; set; }
    }

    public class RefreshTokenDto
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }

    public class LogoutDto
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public bool LogoutAllDevices { get; set; } = false;
        public string Reason { get; set; }
    }

    public class ForgotPasswordDto
    {
        public string Email { get; set; }
        public string RecaptchaToken { get; set; }
    }

    public class VerifyEmailDto
    {
        public string Email { get; set; }
        public string Token { get; set; }
    }

    public class ResendVerificationDto
    {
        public string Email { get; set; }
        public string RecaptchaToken { get; set; }
    }

    public class SsoLoginDto
    {
        public string Provider { get; set; } // Google, Microsoft, LinkedIn, etc.
        public string Token { get; set; }
        public string RedirectUri { get; set; }
        public bool LinkAccount { get; set; } = false;
        public Guid? LinkToUserId { get; set; }
    }

    public class ImpersonateDto
    {
        public Guid UserId { get; set; }
        public string Reason { get; set; }
        public DateTime? ExpiryTime { get; set; }
    }

    public class SessionDto
    {
        public Guid SessionId { get; set; }
        public string DeviceId { get; set; }
        public string DeviceName { get; set; }
        public string DeviceType { get; set; }
        public string Browser { get; set; }
        public string OperatingSystem { get; set; }
        public string IpAddress { get; set; }
        public string Location { get; set; }
        public DateTime LoginTime { get; set; }
        public DateTime? LastActivity { get; set; }
        public DateTime? LogoutTime { get; set; }
        public bool IsCurrent { get; set; }
        public bool IsTrusted { get; set; }
        public string Status { get; set; } // Active, Expired, Terminated
    }
}
