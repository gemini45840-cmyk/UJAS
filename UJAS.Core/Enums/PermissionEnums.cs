using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Core.Enums
{
    public enum RoleType
    {
        SystemAdministrator = 1,
        CompanyAdministrator = 2,
        RegionalManager = 3,
        Manager = 4,
        Applicant = 5
    }

    public enum Permission
    {
        // System Admin permissions
        ManageCompanies = 1,
        AssignCompanyAdmins = 2,
        SetGlobalDefaults = 3,
        GenerateEmbedCodes = 4,
        ViewCrossCompanyAnalytics = 5,
        ManageSystemSettings = 6,

        // Company Admin permissions
        ManageLocations = 10,
        AssignManagers = 11,
        CreateCustomFields = 12,
        HideDefaultFields = 13,
        ManageAssessments = 14,
        ViewCompanyAnalytics = 15,
        ManageBranding = 16,
        SetCompanySettings = 17,

        // Regional Manager permissions
        ViewRegionalApplications = 20,
        ManageRegionalApplications = 21,
        TransferApplications = 22,
        ViewRegionalAnalytics = 23,

        // Manager permissions
        ViewLocationApplications = 30,
        UpdateApplicationStatus = 31,
        AddApplicationComments = 32,
        ViewApplicantDetails = 33,
        ReceiveNotifications = 34,

        // Applicant permissions
        ManageProfile = 40,
        ApplyToJobs = 41,
        ViewApplicationStatus = 42,
        ViewComments = 43,
        UploadDocuments = 44
    }
}
