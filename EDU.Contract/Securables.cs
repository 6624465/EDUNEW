using System;
using System.ComponentModel;

namespace EZY.EDU.Contract
{
    public class Securables : IContract
    {
        // Constructor 
        public Securables() { }

        // Public Members 

        [DisplayName("SecurableItem")]
        public string SecurableItem { get; set; }

        [DisplayName("GroupID")]
        public string GroupID { get; set; }

        [DisplayName("Description")]
        public string Description { get; set; }

        [DisplayName("ActionType")]
        public string ActionType { get; set; }

        [DisplayName("Link")]
        public string Link { get; set; }


        [DisplayName("Icon")]
        public string Icon { get; set; }

        [DisplayName("Sequence")]
        public Int32 Sequence { get; set; }

        [DisplayName("ParentSequence")]
        public Int32 ParentSequence { get; set; }
    }
}
