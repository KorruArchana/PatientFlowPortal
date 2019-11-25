using System.Collections.Generic;

namespace EMIS.PatientFlow.Web.Models
{
    public class TreeItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Depth { get; set; }
        public int ParentId { get; set; }
        public string Path { get; set; }
        public List<TreeItem> Children { get; set; }
        public bool Selected { get; set; }
        public TreeItem()
        {
            Children = new List<TreeItem>();
        }
    }
}