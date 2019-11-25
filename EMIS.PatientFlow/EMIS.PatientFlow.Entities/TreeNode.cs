// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TreeNode.cs" company="">
//   
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace EMIS.PatientFlow.Entities
{
    public class TreeNode
    {
        public string Key { get; set; }
        
        public string Title { get; set; }
       
        public bool Select { get; set; }
       
        public string Unselectable { get; set; }
        
        public bool HideCheckbox { get; set; }
       
        public System.Collections.Generic.List<TreeNode> Children { get; set; }
        public TreeNode()
        {
            Children = new System.Collections.Generic.List<TreeNode>();
        }
    }
}
