using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Core.Enums
{
    public enum Salutation
    {
        Mr,
        Mrs,
        Ms,
        Mx,
        Dr,
        Prof,
        Other
    }

    public enum GenderIdentity
    {
        Male,
        Female,
        NonBinary,
        PreferNotToSay,
        PreferToSelfDescribe
    }

    public enum Ethnicity
    {
        White,
        BlackOrAfricanAmerican,
        HispanicOrLatino,
        Asian,
        NativeAmerican,
        PacificIslander,
        TwoOrMoreRaces,
        PreferNotToAnswer
    }

    public enum VeteranStatus
    {
        ProtectedVeteran,
        DisabledVeteran,
        RecentlySeparatedVeteran,
        ActiveDutyWartime,
        OtherProtectedVeteran,
        NonVeteran,
        PreferNotToSay
    }

    public enum DisabilityStatus
    {
        Yes,
        No,
        PreferNotToSay
    }

    public enum WorkAuthorization
    {
        USCitizen,
        PermanentResident,
        H1BVisa,
        F1Visa,
        J1Visa,
        TNVisa,
        RequiresSponsorship,
        Other
    }

    public enum ContactMethod
    {
        Email,
        Phone,
        Text
    }

    public enum BestTimeToContact
    {
        Morning,
        Afternoon,
        Evening,
        Any
    }

    public enum AddressType
    {
        Home,
        Temporary,
        Permanent,
        Mailing
    }

    public enum EmploymentType
    {
        FullTime,
        PartTime,
        Contract,
        Temporary,
        Internship,
        Seasonal
    }

    public enum WorkSchedule
    {
        Days,
        Evenings,
        Nights,
        Weekends,
        Rotating,
        Flexible
    }

    public enum ShiftAvailability
    {
        First,
        Second,
        Third,
        Any
    }

    public enum InstitutionType
    {
        HighSchool,
        CollegeUniversity,
        TechnicalSchool,
        TradeSchool,
        Online
    }

    public enum DegreeType
    {
        Diploma,
        Associate,
        Bachelor,
        Master,
        PhD,
        Certificate,
        Other
    }

    public enum SkillProficiency
    {
        Beginner,
        Intermediate,
        Advanced,
        Expert
    }

    public enum LanguageProficiency
    {
        Native,
        Fluent,
        Conversational,
        Basic
    }
}
