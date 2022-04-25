using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVC_Task2.Models.VMs
{
    public class FileVM
    {
        [Required]
        public IFormFile[] Files { get; set; }

    }
}
