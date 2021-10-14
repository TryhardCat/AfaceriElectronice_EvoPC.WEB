using System;
using System.Collections.Generic;

#nullable disable

namespace EvoPC.Models.Entities
{
    public partial class SpecialTag : IEntity<int>
    {
        public SpecialTag()
        {
            ProcesorSpecialTags = new HashSet<ProcesorSpecialTag>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ProcesorSpecialTag> ProcesorSpecialTags { get; set; }
    }
}
