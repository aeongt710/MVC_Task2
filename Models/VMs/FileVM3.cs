using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace MVC_Task2.Models.VMs
{
    public class FileVM3
    {
        [Required]
        [Display(Name ="First File")]
        [FileSize(2)]
        [FileExtens(".pdf", ".docx" )]
        public IFormFile File1 { get; set; }

        [Required]
        [Display(Name = "Second File")]
        [FileSize(2)]
        [FileExtens(".pdf", ".docx")]
        public IFormFile File2 { get; set; }

        [Required]
        [Display(Name = "Third File")]
        [FileSize(2)]
        [FileExtens(".pdf", ".docx")]
        public IFormFile File3 { get; set; }
    }
}
