using Microsoft.AspNetCore.Http;

namespace MediumClone.Models
{
    public class ImageVM
    {
        public string ImageName { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}
