Contributing to UJAS Platform
Thank you for considering contributing to the Universal Job Application System (UJAS)! We're building an enterprise-ready, open-core hiring platform using .NET 8.0 and Visual Studio 2022.

🎯 Our Development Philosophy
We follow these core principles:

Enterprise First: All code must meet enterprise standards for security, performance, and maintainability

Clean Architecture: We use Domain-Driven Design (DDD) and Clean Architecture patterns

Test-Driven Development: We expect tests for all new features and bug fixes

Documentation as Code: Code changes must include updated documentation

🛠️ Development Environment Setup
Required Software
Visual Studio 2022 (17.8 or later) with:

ASP.NET and web development workload

.NET desktop development workload

Data storage and processing (for SQL Server tools)

.NET 8.0 SDK

SQL Server 2022 (Developer Edition recommended)

Git for Windows

Node.js 18+ (for frontend assets and build tools)

Environment Setup Script
powershell
# Run as Administrator in PowerShell
Set-ExecutionPolicy -ExecutionPolicy RemoteSigned -Scope CurrentUser

# Clone repository
git clone https://github.com/ujas-platform/core.git
cd ujas-core

# Restore NuGet packages
dotnet restore

# Setup database (using LocalDB for development)
sqllocaldb create "UJAS_Dev"
sqllocaldb start "UJAS_Dev"

# Apply migrations
cd src/Web
dotnet ef database update
Visual Studio 2022 Configuration
Open UJAS.sln in Visual Studio 2022

Set multiple startup projects:

Web (set to Start)

API (set to Start)

Configure SQL Server connection in appsettings.Development.json

Build Solution (Ctrl+Shift+B)

Run (F5) to launch both projects

📁 Solution Structure Understanding
Our solution uses Clean Architecture with these key projects:

Project	Purpose	Dependencies
Core	Domain entities, interfaces, specifications	None
Application	Business logic, DTOs, validators	Core
Infrastructure	Data access, external services	Core, Application
Web	MVC presentation layer	Application, Infrastructure
API	REST API layer	Application, Infrastructure
Shared	Common utilities, extensions	None
🔄 Git Workflow
Branch Strategy
text
main (protected)
├── develop
│   ├── feature/universal-profile-v2
│   ├── feature/qr-code-applications
│   └── bugfix/login-mfa-issue
└── release/v1.2.0
Commit Message Convention
We use Conventional Commits:

text
feat: add universal profile import from LinkedIn
fix(api): resolve null reference in applicant service
docs: update plugin development guide
style: reformat code with dotnet format
refactor: simplify multi-tenancy middleware
test: add unit tests for assessment builder
chore: update NuGet packages to latest
Pull Request Process
Fork the repository

Create a feature branch: git checkout -b feature/amazing-feature

Commit your changes: git commit -m 'feat: add amazing feature'

Push to your fork: git push origin feature/amazing-feature

Open a Pull Request against our develop branch

💻 Coding Standards
C#/.NET 8.0 Standards
csharp
// ✅ DO: Use file-scoped namespaces
namespace UJAS.Core.Entities;

// ✅ DO: Use nullable reference types
public class ApplicantProfile
{
    public required string FirstName { get; set; }
    public string? MiddleName { get; set; }
    public required string LastName { get; set; }
}

// ✅ DO: Use primary constructors (C# 12)
public class JobService(IRepository<Job> repository, ILogger<JobService> logger)
{
    public async Task<Job?> GetByIdAsync(Guid id)
    {
        return await repository.GetByIdAsync(id);
    }
}

// ✅ DO: Use minimal APIs for simple endpoints
app.MapGet("/api/jobs/{id}", async (Guid id, IJobService service) 
    => await service.GetJobAsync(id));

// ❌ DON'T: Use magic strings
// ❌ DON'T: Ignore async/await best practices
// ❌ DON'T: Commit code without proper error handling
Entity Framework Core Standards
csharp
// ✅ DO: Use configurations for entity mappings
public class ApplicantProfileConfiguration : IEntityTypeConfiguration<ApplicantProfile>
{
    public void Configure(EntityTypeBuilder<ApplicantProfile> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Email).IsRequired().HasMaxLength(256);
        builder.HasIndex(a => a.Email).IsUnique();
        
        // Soft delete
        builder.HasQueryFilter(a => !a.IsDeleted);
    }
}

// ✅ DO: Use owned types for value objects
builder.OwnsOne(a => a.Address, address =>
{
    address.Property(a => a.Street).HasMaxLength(200);
    address.Property(a => a.City).HasMaxLength(100);
});
Testing Standards
csharp
// ✅ DO: Write comprehensive unit tests
[Fact]
public async Task SubmitApplication_ValidData_CreatesApplication()
{
    // Arrange
    var mockRepo = new Mock<IApplicationRepository>();
    var service = new ApplicationService(mockRepo.Object);
    var dto = new SubmitApplicationDto { /* valid data */ };
    
    // Act
    var result = await service.SubmitApplicationAsync(dto);
    
    // Assert
    Assert.NotNull(result);
    Assert.Equal(ApplicationStatus.Submitted, result.Status);
    mockRepo.Verify(r => r.AddAsync(It.IsAny<Application>()), Times.Once);
}

// ✅ DO: Use test databases for integration tests
public class ApplicationIntegrationTests : IClassFixture<UJASWebApplicationFactory>
{
    private readonly UJASWebApplicationFactory _factory;
    
    [Fact]
    public async Task GetApplications_ReturnsSuccess()
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync("/api/applications");
        response.EnsureSuccessStatusCode();
    }
}
🧪 Testing Requirements
Test Pyramid
text
       UI Tests (10%)
     /               \
Integration (20%)   E2E (10%)
     \               /
     Unit Tests (60%)
Running Tests
powershell
# Run all tests
dotnet test

# Run specific test project
dotnet test tests/Core.UnitTests

# Run with coverage
dotnet test --collect:"XPlat Code Coverage"

# Run integration tests with test database
dotnet test tests/Web.IntegrationTests --environment Testing
Test Configuration
json
// appsettings.Testing.json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=UJAS_Test_{Guid};Trusted_Connection=True;MultipleActiveResultSets=true"
  },
  "UseInMemoryDatabase": false,
  "EnableTestDataSeeding": true
}
📦 Plugin Development Guidelines
Plugin Structure
text
YourPlugin/
├── YourPlugin.csproj
├── PluginManifest.json
├── Controllers/
├── Services/
├── Views/
├── wwwroot/
├── Migrations/
└── README.md
Manifest File
json
{
  "Id": "com.yourcompany.ujas.assessmentbuilder",
  "Name": "Assessment Builder",
  "Version": "1.0.0",
  "Description": "Create and manage hiring assessments",
  "Author": "Your Company",
  "Company": "Your Company",
  "Website": "https://yourcompany.com",
  "EntryPoint": "YourPlugin.dll",
  "Dependencies": [
    "UJAS.Core >= 1.0.0",
    "UJAS.Application >= 1.0.0"
  ],
  "Permissions": [
    "ManageAssessments",
    "ViewAssessmentResults"
  ],
  "Settings": {
    "EnableProctoring": true,
    "DefaultTimeLimit": 30
  }
}
Plugin Registration
csharp
[UJASPlugin(
    Name = "Assessment Builder",
    Description = "Create hiring assessments",
    Author = "Your Company")]
public class AssessmentBuilderPlugin : IUJASPlugin
{
    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IAssessmentService, AssessmentService>();
        services.AddControllersWithViews()
                .AddApplicationPart(typeof(AssessmentBuilderPlugin).Assembly);
    }
    
    public void Configure(IApplicationBuilder app)
    {
        // Register middleware, routes, etc.
    }
}
🔍 Code Review Checklist
Before submitting a PR, ensure:

Security
No sensitive data in code or commits

Input validation implemented

SQL injection prevented (use parameters)

Authentication/authorization checks present

No hardcoded secrets

Performance
Database queries optimized (check execution plans)

Proper indexing implemented

Caching used where appropriate

Async/await used for I/O operations

No memory leaks (IDisposable implemented)

Quality
Code follows our style guide

Unit tests written (minimum 80% coverage)

Integration tests for API endpoints

No compiler warnings

XML documentation for public APIs

Enterprise Readiness
Multi-tenancy considerations addressed

Audit logging implemented

Error handling comprehensive

Graceful degradation for failures

Configuration externalized

🚀 Building and Deployment
Build Script
powershell
# build.ps1
param([string]$Configuration = "Release")

Write-Host "Building UJAS Platform..." -ForegroundColor Green

# Clean
dotnet clean --configuration $Configuration

# Restore
dotnet restore

# Build
dotnet build --configuration $Configuration --no-restore

# Test
dotnet test --configuration $Configuration --no-build

# Publish
dotnet publish src/Web --configuration $Configuration --output ./publish

Write-Host "Build completed successfully!" -ForegroundColor Green
CI/CD Pipeline (GitHub Actions)
yaml
name: .NET CI/CD
on: [push, pull_request]

jobs:
  build:
    runs-on: windows-latest
    steps:
    - uses: actions/checkout@v3
    
    - name: Setup .NET
      uses: actions/setup-dotnet@v3
      with:
        dotnet-version: '8.0.x'
        
    - name: Setup SQL Server
      uses: actions/setup-sqlserver@v1
      
    - name: Build
      run: dotnet build --configuration Release
      
    - name: Test
      run: dotnet test --configuration Release --verbosity normal
📚 Documentation Requirements
Code Documentation
csharp
/// <summary>
/// Service for managing job applications in the UJAS platform.
/// Implements multi-tenant application processing with audit logging.
/// </summary>
/// <remarks>
/// This service handles the complete application lifecycle from
/// submission through to hiring decision.
/// </remarks>
public class ApplicationService : IApplicationService
{
    /// <summary>
    /// Submits a new job application with validation and audit logging.
    /// </summary>
    /// <param name="dto">Application data transfer object</param>
    /// <param name="applicantId">Unique identifier of the applicant</param>
    /// <returns>
    /// ApplicationSubmission object with tracking ID and status
    /// </returns>
    /// <exception cref="ValidationException">
    /// Thrown when application data fails validation
    /// </exception>
    /// <exception cref="TenantAccessException">
    /// Thrown when applicant doesn't have access to the specified job's tenant
    /// </exception>
    public async Task<ApplicationSubmission> SubmitApplicationAsync(
        SubmitApplicationDto dto, 
        Guid applicantId)
    {
        // Implementation
    }
}
🆘 Getting Help
Community Resources
Discord: Join our Discord server

GitHub Discussions: Check Discussions tab

Stack Overflow: Use tag ujas-platform

Issue Reporting
When reporting bugs:

Check if issue already exists

Use bug report template

Include:

Steps to reproduce

Expected vs actual behavior

Environment details

Screenshots if applicable

📊 Contributor Recognition
We recognize contributions through:

Contributor Hall of Fame in our documentation

Special badges in GitHub discussions

Early access to new features

Swag for significant contributions

📄 License Headers
All source files must include:

csharp
// Copyright (c) UJAS Platform Contributors.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.
Thank you for contributing to UJAS Platform! Together, we're building the future of hiring.
