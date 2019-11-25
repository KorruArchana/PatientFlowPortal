namespace EMIS.PatientFlow.Entities
{
    public class AccessMapping
    {
        public int Id { get; set; }
        public int AccessTypeId { get; set; }
        public string UserId { get; set; }
        public bool AddAccess { get; set; }
        public bool EditAccess { get; set; }
        public bool DeleteAccess { get; set; }
        public bool ViewAccess { get; set; }
        public string Name { get; set; }
    }
}
