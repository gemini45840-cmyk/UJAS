using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Users.DTOs
{
    public class RoleDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string RoleType { get; set; } // System, Company, Location
        public Guid? CompanyId { get; set; }
        public string CompanyName { get; set; }
        public Guid? LocationId { get; set; }
        public string LocationName { get; set; }
        public bool IsSystemRole { get; set; }
        public bool IsDefault { get; set; }
        public int UserCount { get; set; }
        public List<string> Permissions { get; set; } = new();
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public string CreatedBy { get; set; }
    }

    public class RoleDetailDto : RoleDto
    {
        public List<RolePermissionDto> RolePermissions { get; set; } = new();
        public List<RoleUserDto> AssignedUsers { get; set; } = new();
        public Dictionary<string, List<string>> PermissionCategories { get; set; } = new();
    }

    public class RolePermissionDto
    {
        public string Permission { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public bool IsGranted { get; set; }
        public DateTime? GrantedDate { get; set; }
        public string GrantedBy { get; set; }
    }

    public class RoleUserDto
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public DateTime AssignedDate { get; set; }
        public string AssignedBy { get; set; }
    }

    public class PermissionDto
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string Module { get; set; } // Applications, Assessments, Companies, Users, etc.
        public bool IsSystemPermission { get; set; }
        public string RequiredRole { get; set; }
        public List<string> DependentPermissions { get; set; } = new();
        public bool IsDangerous { get; set; }
        public string WarningMessage { get; set; }
    }

    public class RoleCreateDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string RoleType { get; set; } // System, Company, Location
        public Guid? CompanyId { get; set; }
        public Guid? LocationId { get; set; }
        public bool IsDefault { get; set; } = false;
        public List<string> Permissions { get; set; } = new();
        public Guid CreatedBy { get; set; }
    }

    public class RoleUpdateDto
    {
        public Guid RoleId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool? IsDefault { get; set; }
        public List<string> AddPermissions { get; set; } = new();
        public List<string> RemovePermissions { get; set; } = new();
        public Guid UpdatedBy { get; set; }
    }
}
