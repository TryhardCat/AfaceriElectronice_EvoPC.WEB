using System;
using System.Collections.Generic;

#nullable disable

namespace EvoPC.Models.Entities
{
    public partial class SocketType : IEntity<int>
    {
        public SocketType()
        {
            Procesors = new HashSet<Procesor>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Procesor> Procesors { get; set; }
    }
}
