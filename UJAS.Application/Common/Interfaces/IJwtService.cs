using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UJAS.Core.Entities.User;

namespace UJAS.Application.Common.Interfaces
{
    public interface IJwtService
    {
        Task<string> GenerateTokenAsync(tUser user);
        string GenerateRefreshToken();
        Task<tUser> GetUserFromTokenAsync(string token);
        Task<bool> ValidateTokenAsync(string token);
        Task<ClaimsPrincipal> GetPrincipalFromTokenAsync(string token);
    }
}
