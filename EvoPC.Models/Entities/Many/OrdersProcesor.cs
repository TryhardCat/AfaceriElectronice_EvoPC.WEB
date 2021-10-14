using System;
using System.Collections.Generic;

#nullable disable

namespace EvoPC.Models.Entities
{
    public partial class OrdersProcesor
    {
        public int ProcesorId { get; set; }
        public int OrderId { get; set; }

        public virtual Order Order { get; set; }
        public virtual Procesor Procesor { get; set; }
    }
}
