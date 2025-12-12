Contributing to UJAS Platform
Thank you for considering contributing to the Universal Job Application System (UJAS)! We're building an enterprise-ready, open-core hiring platform using .NET 8.0 and Visual Studio 2022.

Our Development Philosophy
We follow these core principles:

Enterprise First: All code must meet enterprise standards for security, performance, and maintainability
Clean Architecture: We use Domain-Driven Design (DDD) and Clean Architecture patterns
Test-Driven Development: We expect tests for all new features and bug fixes
Documentation as Code: Code changes must include updated documentation

Development Environment Setup
Required Software
Visual Studio 2022 (17.8 or later) with:
ASP.NET and web development workload
.NET desktop development workload
Data storage and processing (for SQL Server tools)
.NET 8.0 SDK
SQL Server 2022 (Developer Edition recommended)
Git for Windows
Node.js 18+ (for frontend assets and build tools)

Code Review Checklist
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



