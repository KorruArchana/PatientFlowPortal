namespace EMIS.PatientFlow.Common.Enums
{
    /// <summary>
    ///  Node Types in TreeView
    /// </summary>
    public enum NodeType
    {
        Organisation = 1,
        Departments = 2,
        Members = 3,
        Diverts = 4,
        Alerts = 5,
        Kiosks = 6,
        Reports = 8,
        Users = 9,
        SyncServices = 37,

        KioskDetails = 10,
        EditKiosk = 11,
        AddKiosk = 12,

        Questionnaires = 7,
        AddQuestionnaires = 13,
        EditQuestionnaire = 14,

        AddQuestions = 15,
        EditQuestions = 16,
        QuestionnaireDetails = 17,
        DeleteQuestions = 18,
        DeleteQuestionnaire = 19,
        AddAlerts = 20,
        EditAlerts = 21,
        AlertDetails = 22,
        OrganisationDetails = 23,
        EditOrganisation = 24,
        AddOrganisation = 25,
        DeleteOrganisation = 26,
        DeleteAlert = 27,

        AddDepartment = 28,
        EditDepartment = 29,
        DeleteDepartment = 30,
        DepartmentDetails = 31,

        AddMember = 32,
        EditMember = 33,
        DeleteMember = 34,
        MemberDetails = 35,

        AddUser = 36,
        AddSyncService = 38,
        EditService = 39,
        DeleteSyncService = 40,
        DeleteUser = 41,
        Roles = 42,
        Appointments = 43,

        AddDivert = 44,
        EditDivert = 45,
        DivertDetails = 46,
        DeleteDivert = 47,

        PatientMessage = 48,
        AddPatient = 49,
        EditPatient = 50,
        PatientDetails = 51,
        DeletePatient = 52,
        TranslationList = 53,
        ReportDetails = 54
    }
}
