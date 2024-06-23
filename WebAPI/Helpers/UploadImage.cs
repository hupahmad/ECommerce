using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Helpers
{
    public class UploadImage
    {
        public string FileName { get; set; } = "";
        [Required]
        public required IFormFile File { get; set; }
    }
}