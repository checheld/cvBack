using Data.Repositories.Abstract;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

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
                var newModel = new UserEntity
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Description = user.Description,
                    CreatedAt = user.CreatedAt,
                    photoUrl = user.photoUrl,
                };
                var technologies = user.TechnologyList;

                await db.Users.AddAsync(newModel);
                await db.SaveChangesAsync();

                var addedUser = await db.Users.Where(x => x.CreatedAt == newModel.CreatedAt)
                    .FirstOrDefaultAsync();

                var educationList = new List<EducationEntity>();
                var educations = user.EducationList;
                foreach (var education in educations)
                {
                    var findUniversity = await db.Universities.Where(x => x.Id == education.UniversityId)
                    .FirstOrDefaultAsync();
                    var newEducation = new EducationEntity
                    {
                        Speciality = education.Speciality,
                        StartDate = education.StartDate,
                        EndDate = education.EndDate,
                        University = findUniversity,
                        UserId = addedUser.Id,
                    };
                    educationList.Add(newEducation);
                }
                await db.Educations.AddRangeAsync(educationList);

                var workExperienceList = new List<WorkExperienceEntity>();
                var workExperiences = user.WorkExperienceList;
                foreach (var workExperience in workExperiences)
                {
                    var findCompany = await db.Companies.Where(x => x.Id == workExperience.CompanyId)
                    .FirstOrDefaultAsync();
                    var newWorkExperience = new WorkExperienceEntity
                    {
                        Position = workExperience.Position,
                        StartDate = workExperience.StartDate,
                        EndDate = workExperience.EndDate,
                        Description = workExperience.Description,
                        Company = findCompany,
                        UserId = addedUser.Id
                    };
                    workExperienceList.Add(newWorkExperience);
                }
                await db.WorkExperiences.AddRangeAsync(workExperienceList);

                var links = new List<UserTechnologyEntity>();

                foreach (var technology in technologies)
                {
                    links.Add(new UserTechnologyEntity
                    {
                        UserId = addedUser.Id,
                        TechnologyId = technology.Id
                    }
                    );
                }
                await db.UserTechnology.AddRangeAsync(links);

                await db.SaveChangesAsync();

                user.TechnologyList.Select(c => { c.UserList = null; return c; }).ToList();

                return await GetUserById(newModel.Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> DeleteUserById(int id)
        {
            var foundUser = await db.Users.SingleOrDefaultAsync(x => x.Id == id);
            if (foundUser != null)
            {
                db.Users.Remove(foundUser);
                await db.SaveChangesAsync();
            }
            return null;
        }

        public async Task<List<UserEntity>> GetAllUsers()
        {
            var users = await db.Users
                .Include(x => x.TechnologyList)
                .Include(x => x.EducationList)
                .ThenInclude(x => x.University)
                .Include(x => x.WorkExperienceList)
                .ThenInclude(x => x.Company)
                .ToListAsync();

            if (users != null)
            {
                return users;
            }
            return null;
        }

        public async Task<UserEntity> GetUserById(int id)
        {
            var user = await db.Users.Include(x => x.TechnologyList)
                .Include(x => x.EducationList).ThenInclude(x => x.University)
                .Include(x => x.WorkExperienceList).ThenInclude(x => x.Company).AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == id);

            if (user != null)
            {
                return user;
            }
            return null;
        }

        public async Task<List<UserEntity>> GetUsersBySearch(string search)
        {
            try
            {
                return await db.Users.Include(x => x.TechnologyList)
                    .Include(x => x.EducationList).ThenInclude(x => x.University)
                    .Include(x => x.WorkExperienceList).ThenInclude(x => x.Company)
                    .Where(user => user.FirstName.Trim().ToLower().Contains(search) || user.LastName.Trim().ToLower().Contains(search))
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        public async Task RemoveAllEducations(int userId)
        {

            var get = await this.db.Educations.Where(x => x.UserId == userId).ToListAsync();

            this.db.Educations.RemoveRange(get);
            await db.SaveChangesAsync();
        }

        public async Task RemoveAllWorkExperiences(int userId)
        {

            var get = await this.db.WorkExperiences.Where(x => x.UserId == userId).ToListAsync();

            this.db.WorkExperiences.RemoveRange(get);
            await db.SaveChangesAsync();
        }

        public async Task<UserEntity> UpdateUser(UserEntity user)
        {
            try
            {
                var newModel = new UserEntity
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Description = user.Description,
                    photoUrl = user.photoUrl,
                    Id = user.Id
                };
                db.Users.Update(newModel);
                var technologies = user.TechnologyList;
               
                var addedUser = await db.Users.Where(x => x.Id == newModel.Id)
                    .FirstOrDefaultAsync();

                var educationList = new List<EducationEntity>();
                var educations = user.EducationList;
                foreach (var education in educations)
                {
                    var findUniversity = await db.Universities.Where(x => x.Id == education.UniversityId)
                    .FirstOrDefaultAsync();
                    var newEducation = new EducationEntity
                    {
                        Speciality = education.Speciality,
                        StartDate = education.StartDate,
                        EndDate = education.EndDate,
                        University = findUniversity,
                        UserId = addedUser.Id,
                        CreatedAt = education.CreatedAt
                    };
                    educationList.Add(newEducation);
                }
                await db.Educations.AddRangeAsync(educationList);
                await db.SaveChangesAsync();

                var workExperienceList = new List<WorkExperienceEntity>();
                var workExperiences = user.WorkExperienceList;
                foreach (var workExperience in workExperiences)
                {
                    var findCompany = await db.Companies.Where(x => x.Id == workExperience.CompanyId)
                    .FirstOrDefaultAsync();
                    var newWorkExperience = new WorkExperienceEntity
                    {
                        Position = workExperience.Position,
                        StartDate = workExperience.StartDate,
                        EndDate = workExperience.EndDate,
                        Description = workExperience.Description,
                        Company = findCompany,
                        UserId = addedUser.Id,
                        CreatedAt = workExperience.CreatedAt
                    };
                    workExperienceList.Add(newWorkExperience);
                }
                await db.WorkExperiences.AddRangeAsync(workExperienceList);
                await db.SaveChangesAsync();

                var findededConnection = await db.UserTechnology.Where(x => x.UserId == user.Id)
                   .ToListAsync();
                db.UserTechnology.RemoveRange(findededConnection);
                await db.SaveChangesAsync();

                var links = new List<UserTechnologyEntity>();

                foreach (var technology in technologies)
                {
                    links.Add(new UserTechnologyEntity
                    {
                        UserId = addedUser.Id,
                        TechnologyId = technology.Id
                    }
                    );
                }
                await db.UserTechnology.AddRangeAsync(links);

                await db.SaveChangesAsync();
                
                await db.SaveChangesAsync();
                user.TechnologyList.Select(c => { c.UserList = null; return c; }).ToList();

                return await GetUserById(newModel.Id);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}