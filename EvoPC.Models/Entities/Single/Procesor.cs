using System;
using System.Collections.Generic;

#nullable disable

namespace EvoPC.Models.Entities
{
    public partial class Procesor : IEntity<int>
    {
        public Procesor()
        {
            Feedbacks = new HashSet<Feedback>();
            OrdersProcesors = new HashSet<OrdersProcesor>();
            ProcesorSpecialTags = new HashSet<ProcesorSpecialTag>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Pret { get; set; }
        public bool EsteInStoc { get; set; }
        public string ImagePath { get; set; }
        public int SocketTypeId { get; set; }

        public virtual SocketType Socket { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; }
        public virtual ICollection<OrdersProcesor> OrdersProcesors { get; set; }
        public virtual ICollection<ProcesorSpecialTag> ProcesorSpecialTags { get; set; }
    }
}
