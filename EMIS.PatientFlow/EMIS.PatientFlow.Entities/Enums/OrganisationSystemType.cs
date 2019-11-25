
namespace EMIS.PatientFlow.Entities.Enums
{
    public enum OrganisationSystemType
    {
        EmisWeb = 1,
        EmisPcs = 2,
        Pas = 3,
        None = 4,
        Topas = 5,
        EmisPcsLan = 6
    }

    public static class Constants
    {
        public const int EmisWeb = 1;
        public const int EmisPcs = 2;
        public const int EmisPcsLan = 6;
        public const int Topas = 5;
    }
}
