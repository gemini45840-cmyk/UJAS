namespace UJAS.Application.Companies.Dtos
{
    public class CompanyUserDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CompanyId { get; set; }
        public int? LocationId { get; set; }
        public string UserEmail { get; set; }
        public string UserFullName { get; set; }
        public bool IsCompanyAdmin { get; set; }
        public bool IsRegionalManager { get; set; }
        public bool IsManager { get; set; }
        public string EmployeeId { get; set; }
        public string Department { get; set; }
        public string Title { get; set; }
        public string AssignedLocationName { get; set; }
        public List<int> RegionalLocationIds { get; set; } = new();
        public List<string> RegionalLocationNames { get; set; } = new();
        public DateTime CreatedAt { get; set; }
    }
}