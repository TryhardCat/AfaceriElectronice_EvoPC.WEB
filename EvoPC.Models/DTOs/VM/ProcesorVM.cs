using EvoPC.Models.Attributes;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EvoPC.Models.DTOs.VM
{
    public class ProcesorVM
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(256)]
        public string Name { get; set; }
        [Required]
        [MaxLength(2000)]
        public string Description { get; set; }
        public decimal Pret { get; set; }
        public bool EsteInStoc { get; set; }
        public string ImagePath { get; set; }
        [Required]
        public int SocketTypeId { get; set; }
        public string SocketTypeName { get; set; }

        public List<IdNameDto>SocketTypes { get; set; }

        [AllowedExtensions(".jpg", ".png", ".jpeg")]
        [MaxFileSize(3 * 1024 * 1024)]
        public IFormFile ProcImage { get; set; }

    }
} 
