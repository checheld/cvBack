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
                
                db.Entry(newModel).State = EntityState.Modified;
                await db.SaveChangesAsync();
                db.Entry(newModel).State = EntityState.Detached;

                var addedUser = await db.Users.Where(x => x.Id == user.Id).Include(x => x.EducationList).Include(x => x.WorkExperienceList).AsNoTracking()
                    .FirstOrDefaultAsync();

                var technologies = user.TechnologyList;
                var educations = user.EducationList;
                var workExperiences = user.WorkExperienceList;
                var educationList = new List<EducationEntity>();
                var workExperienceList = new List<WorkExperienceEntity>();

                if (user.WorkExperienceList.Count() < addedUser.WorkExperienceList.Count())
                {
                    var deleteWorkExperiences = addedUser.WorkExperienceList.ExceptBy(workExperiences.Select(ed => ed.Id), x => x.Id).ToList();
                    this.db.WorkExperiences.RemoveRange(deleteWorkExperiences);
                }

                if (user.EducationList.Count() < addedUser.EducationList.Count())
                {
                    var deleteEducations = addedUser.EducationList.ExceptBy(educations.Select(ed => ed.Id), x => x.Id).ToList();
                    this.db.Educations.RemoveRange(deleteEducations);
                }

                foreach (var education in educations)
                {
                    var findUniversity = await db.Universities.Where(x => x.Id == education.UniversityId)
                    .FirstOrDefaultAsync();

                    var findEducation = await db.Educations.Where(x => x.Id == education.Id).FirstOrDefaultAsync();

                    if (findEducation != null)
                    {
                        findEducation.Speciality = education.Speciality;
                        findEducation.StartDate = education.StartDate;
                        findEducation.EndDate = education.EndDate;
                        findEducation.University = findUniversity;
                        findEducation.UniversityId = findUniversity.Id;
                        findEducation.UserId = addedUser.Id;
                        findEducation.CreatedAt = education.CreatedAt;
                    }
                    else
                    {
                        var newEducation = new EducationEntity
                        {
                            Speciality = education.Speciality,
                            StartDate = education.StartDate,
                            EndDate = education.EndDate,
                            University = findUniversity,
                            UniversityId = findUniversity.Id,
                            UserId = addedUser.Id,
                            CreatedAt = education.CreatedAt
                        };
                        educationList.Add(newEducation);
                    }
                }
                await db.Educations.AddRangeAsync(educationList);
                await db.SaveChangesAsync();

                foreach (var workExperience in workExperiences)
                {
                    var findCompany = await db.Companies.Where(x => x.Id == workExperience.CompanyId)
                    .FirstOrDefaultAsync();

                    var findWorkExperience = await db.WorkExperiences.Where(x => x.Id == workExperience.Id).FirstOrDefaultAsync();

                    if (findWorkExperience != null)
                    {
                        findWorkExperience.Position = workExperience.Position;
                        findWorkExperience.Description = workExperience.Description;
                        findWorkExperience.StartDate = workExperience.StartDate;
                        findWorkExperience.EndDate = workExperience.EndDate;
                        findWorkExperience.Company = findCompany;
                        findWorkExperience.CompanyId = findCompany.Id;
                        findWorkExperience.UserId = addedUser.Id;
                        findWorkExperience.CreatedAt = workExperience.CreatedAt;
                    }
                    else
                    {
                        var newWorkExperience = new WorkExperienceEntity
                        {
                            Position = workExperience.Position,
                            Description = workExperience.Description,
                            StartDate = workExperience.StartDate,
                            EndDate = workExperience.EndDate,
                            Company = findCompany,
                            CompanyId = findCompany.Id,
                            UserId = addedUser.Id,
                            CreatedAt = workExperience.CreatedAt
                        };
                        workExperienceList.Add(newWorkExperience);
                    }
                }
                await db.WorkExperiences.AddRangeAsync(workExperienceList);
                await db.SaveChangesAsync();

                var findededConnection = await db.UserTechnology.Where(x => x.UserId == user.Id)
                   .ToListAsync();
                db.UserTechnology.RemoveRange(findededConnection);
                await db.SaveChangesAsync();

                var links = new List<UserTechnologyEntity>();

                foreach (var technology in user.TechnologyList)
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

                return await GetUserById(user.Id);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}