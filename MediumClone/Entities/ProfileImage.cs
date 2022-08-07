using MediumClone.Models.Authentication;
using Microsoft.AspNetCore.Http;
using System.ComponentModel;

namespace MediumClone.Entities
{
    public class ProfileImage
    {
        public int ImageId { get; set; }
        [DisplayName("Image Name")]
        public string ImageName { get; set; }
        [DisplayName("Image File")]
        public IFormFile ImageFile { get; set; }
        public string UserId { get; set; }
        public  AppUser User { get; set; }
    }
}
