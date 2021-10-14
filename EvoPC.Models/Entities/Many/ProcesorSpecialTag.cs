using System;
using System.Collections.Generic;

#nullable disable

namespace EvoPC.Models.Entities
{
    public partial class ProcesorSpecialTag
    {
        public int SpecialTagId { get; set; }
        public int ProcesorId { get; set; }

        public virtual Procesor Procesor { get; set; }
        public virtual SpecialTag SpecialTag { get; set; }
    }
}
