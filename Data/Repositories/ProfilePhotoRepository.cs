#region Imports
using Data.Entities;
using Data.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
#endregion

namespace Data.Repositories
{
    public class ProfilePhotoRepository : IProfilePhotoRepository
    {
        private ApplicationContext db;
        public ProfilePhotoRepository(IServiceProvider _serviceProvider)
        {
            db = _serviceProvider.GetService<ApplicationContext>();
        }

        public async Task<PhotoParamsEntity> AddPhotoParams(PhotoParamsEntity photoParams)
        {
            try
            {
                await db.PhotoParams.AddAsync(photoParams);
                await db.SaveChangesAsync();

                var addedPhotoParams = await db.PhotoParams.Where(x => x.CreatedAt == photoParams.CreatedAt)
                   .FirstOrDefaultAsync();

                return await GetPhotoParamsById(addedPhotoParams.Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<PhotoParamsEntity> GetPhotoParamsById(int id)
        {
            try
            {
                return await db.PhotoParams.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
}
        public async Task<PhotoParamsEntity> UpdatePhotoParams(PhotoParamsEntity photoParams)
        {
            try
            {
                db.PhotoParams.Update(photoParams);
                await db.SaveChangesAsync();

                var addedPhotoParams = await db.PhotoParams.Where(x => x.CreatedAt == photoParams.CreatedAt)
                  .FirstOrDefaultAsync();

                return await GetPhotoParamsById(addedPhotoParams.Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}