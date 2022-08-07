using MediumClone.Entities;
using MediumClone.Models.Context;
using MediumClone.Repositories.Abstract;
using System.Collections.Generic;
using System.Linq;

namespace MediumClone.Repositories.Concrete
{
    public class ImageRepository : GenericRepository<ProfileImage>,IImageRepository
    {
        private readonly AppDbContext db;
        public ImageRepository(AppDbContext db) : base(db)
        {
            this.db = db;
        }

        public IEnumerable<ProfileImage> GetAllImagesByUserId(string userId)
        {
            return db.Set<ProfileImage>().Where(a => a.UserId == userId).ToList();
        }

        public ProfileImage GetImageByUserId(string userId)
        {
            return db.Set<ProfileImage>().Where(a=>a.UserId == userId).FirstOrDefault();
        }       
    }
}
