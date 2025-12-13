namespace UJAS.Application.Locations.Dtos
{
    public class LocationDto
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string StateProvince { get; set; }
        public string ZipPostalCode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public bool IsHeadquarters { get; set; }
        public bool IsActive { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public LocationStatisticsDto Statistics { get; set; }
        public List<ManagerDto> Managers { get; set; } = new();
    }

    public class CreateLocationDto
    {
        public int CompanyId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string StateProvince { get; set; }
        public string ZipPostalCode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public bool IsHeadquarters { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
    }

    public class UpdateLocationDto
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string StateProvince { get; set; }
        public string ZipPostalCode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public bool IsHeadquarters { get; set; }
        public bool IsActive { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
    }

    public class LocationStatisticsDto
    {
        public int TotalApplications { get; set; }
        public int ActiveApplications { get; set; }
        public int ApplicationsThisMonth { get; set; }
        public Dictionary<string, int> ApplicationsByStatus { get; set; } = new();
        public Dictionary<string, int> ApplicationsByPosition { get; set; } = new();
        public double AverageProcessingTime { get; set; } // in days
    }

    public class ManagerDto
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime AssignedDate { get; set; }
    }

    public class AssignManagerDto
    {
        public int LocationId { get; set; }
        public int UserId { get; set; }
        public bool IsPrimaryManager { get; set; }
    }

    public class LocationFilterDto
    {
        public int? CompanyId { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsHeadquarters { get; set; }
        public string Country { get; set; }
        public string StateProvince { get; set; }
        public string City { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public double? RadiusInMiles { get; set; }
    }
}