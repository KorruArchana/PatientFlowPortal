namespace EMIS.PatientFlow.Entities
{
    public class SiteMenu : Entity
    {
        public string NavUrl { get; set; }
        public int NodeTypeId { get; set; }
        public int NodeId { get; set; }
        public int ParentMenuId { get; set; }
        public string Selected { get; set; }
        public int Depth { get; set; }
        public string Path { get; set; }
    }
}
