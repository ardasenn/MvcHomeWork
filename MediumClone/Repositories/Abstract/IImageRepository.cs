using MediumClone.Entities;
using System.Collections.Generic;

namespace MediumClone.Repositories.Abstract
{
    public interface IImageRepository :IRepository<ProfileImage>
    {
        IEnumerable<ProfileImage> GetAllImagesByUserId(string userId);
       ProfileImage GetImageByUserId(string userId);
    }
}
