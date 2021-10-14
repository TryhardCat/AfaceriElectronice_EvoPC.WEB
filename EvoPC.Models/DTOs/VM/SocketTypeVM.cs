using System.ComponentModel.DataAnnotations;

namespace EvoPC.Models.DTOs.VM
{
     public class SocketTypeVM
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
    }
}
