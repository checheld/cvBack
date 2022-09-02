#region Imports
using Data.Repositories.Abstract;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
#endregion

namespace Data.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private ApplicationContext db;
        public UsersRepository(IServiceProvider _serviceProvider)
        {
            db = _serviceProvider.GetService<ApplicationContext>();
        }

        public async Task<UserEntity> AddUser(UserEntity user)
        {
            try
            {
                await db.Users.AddAsync(user);
                await db.SaveChangesAsync();

                var addedUser = await db.Users.Where(x => x.CreatedAt == user.CreatedAt)
                    .FirstOrDefaultAsync();

                return await GetUserById(addedUser.Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task AddUserTechnology(List<UserTechnologyEntity> userTechnology)
        {
            try
            {
                await db.UserTechnology.AddRangeAsync(userTechnology);
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task AddEducations(List<EducationEntity> education)
        {
            try
            {
                await db.Educations.AddRangeAsync(education);
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task AddWorkExperiences(List<WorkExperienceEntity> workExperience)
        {
            try
            {
                await db.WorkExperiences.AddRangeAsync(workExperience);
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> DeleteUserById(int id)
        {
            try
            {
                db.Users.Remove(await db.Users.SingleOrDefaultAsync(x => x.Id == id));
                await db.SaveChangesAsync();

                return id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<UserEntity>> GetAllUsers()
        {
            try
            {
                return await db.Users.Include(x => x.TechnologyList)
                   .Include(x => x.EducationList).ThenInclude(x => x.University)
                   .Include(x => x.WorkExperienceList).ThenInclude(x => x.Company)
                   .Include(x => x.PhotoParams).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<UserEntity> GetUserById(int id)
        {
            try
            {
                var user = await db.Users.Include(x => x.TechnologyList)
                   .Include(x => x.EducationList).ThenInclude(x => x.University)
                   .Include(x => x.WorkExperienceList).ThenInclude(x => x.Company)
                   .Include(x => x.PhotoParams).AsNoTracking()
                   .SingleOrDefaultAsync(x => x.Id == id);

                return user;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<EducationEntity> GetEducationById(int id)
        {
            try
            {
                return await db.Educations.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<WorkExperienceEntity> GetWorkExperienceById(int id)
        {
            try
            {
                return await db.WorkExperiences.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<UserEntity>> GetUsersBySearch(string search)
        {
            try
            {
                return await db.Users.Include(x => x.TechnologyList)
                    .Include(x => x.EducationList).ThenInclude(x => x.University)
                    .Include(x => x.WorkExperienceList).ThenInclude(x => x.Company)
                    .Include(x => x.PhotoParams)
                    .Where(user => user.FirstName.Trim().ToLower().Contains(search) || user.LastName.Trim().ToLower().Contains(search))
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task RemoveAllEducations(int userId)
        {
            try
            {
                this.db.Educations.RemoveRange(await this.db.Educations.Where(x => x.UserId == userId).ToListAsync());
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task RemoveAllWorkExperiences(int userId)
        {
            try
            {
                this.db.WorkExperiences.RemoveRange(await this.db.WorkExperiences.Where(x => x.UserId == userId).ToListAsync());
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task DeleteWorkExperience(int workExpId)
        {
            try
            {
                db.WorkExperiences.Remove(await db.WorkExperiences.SingleOrDefaultAsync(x => x.Id == workExpId));
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task DeleteEducation(int educationId)
        {
            try
            {
                db.Educations.Remove(await db.Educations.SingleOrDefaultAsync(x => x.Id == educationId));
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<UserEntity> UpdateUser(UserEntity user)
        {
            try
            {
                db.UserTechnology.RemoveRange(await db.UserTechnology.Where(x => x.UserId == user.Id).ToListAsync());
                await db.SaveChangesAsync();

                db.Entry(user).State = EntityState.Modified;
                await db.SaveChangesAsync();
                db.Entry(user).State = EntityState.Detached;

                return await GetUserById(user.Id);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<EducationEntity> UpdateEducation(EducationEntity education)
        {
            try
            {
                db.Educations.Update(education);
                await db.SaveChangesAsync();

                return education;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<WorkExperienceEntity> UpdateWorkExperience(WorkExperienceEntity workExperience)
        {
            try
            {
                db.WorkExperiences.Update(workExperience);
                await db.SaveChangesAsync();

                return workExperience;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}