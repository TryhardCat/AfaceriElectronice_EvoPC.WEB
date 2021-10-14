using System;
using System.Collections.Generic;

#nullable disable

namespace EvoPC.Models.Entities
{
    public partial class Order : IEntity<int>
    {
        public Order()
        {
            OrdersProcesors = new HashSet<OrdersProcesor>();
        }

        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhoneNumber { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerAddress { get; set; }
        public DateTime OrderDate { get; set; }

        public virtual ICollection<OrdersProcesor> OrdersProcesors { get; set; }
    }
}
