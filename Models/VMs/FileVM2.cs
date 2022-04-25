using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVC_Task2.Models.VMs
{
    public class FileVM2
    {
        [Required]
        [MaxFiles(3)]
        [FileExtens(new string[]{".pdf",".docx"})]
        [FileSize(2)]
        public IEnumerable<IFormFile> Files { get; set; }
    }
}
