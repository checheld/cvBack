using Data.Repositories.Abstract;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Data.Repositories
{
    public class CVsRepository : ICVsRepository
    {
        private ApplicationContext db;
        public CVsRepository(IServiceProvider _serviceProvider)
        {
            db = _serviceProvider.GetService<ApplicationContext>();
        }

        public async Task<CVEntity> AddCV(CVEntity cv)
        {
            try
            {
                var findUser = await db.Users.Where(x => x.Id == cv.UserId)
                   .FirstOrDefaultAsync();

                var newModel = new CVEntity
                {
                    CVName = cv.CVName,
                    CreatedAt = cv.CreatedAt,
                    User = findUser
                };

                await db.CVs.AddAsync(newModel);
                await db.SaveChangesAsync();

                var addedCV = await db.CVs.Where(x => x.CreatedAt == newModel.CreatedAt)
                    .FirstOrDefaultAsync();

                var projectCVList = new List<ProjectCVEntity>();
                var projectCVs = cv.ProjectCVList;
                foreach (var projectCV in projectCVs)
                {
                    var findProject = await db.Projects.Where(x => x.Id == projectCV.ProjectId)
                    .FirstOrDefaultAsync();
                    var newProjectCV = new ProjectCVEntity
                    {
                        Position = projectCV.Position,
                        Description = projectCV.Description,
                        StartDate = projectCV.StartDate,
                        EndDate = projectCV.EndDate,
                        Project = findProject,
                        CVId = addedCV.Id
                    };
                    projectCVList.Add(newProjectCV);
                    await db.SaveChangesAsync();
                }
                await db.ProjectCVs.AddRangeAsync(projectCVList);
                await db.SaveChangesAsync();
                return await GetCVById(newModel.Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> DeleteCVById(int id)
        {
            var foundCV = await db.CVs.SingleOrDefaultAsync(x => x.Id == id);
            if (foundCV != null)
            {
                db.CVs.Remove(foundCV);
                await db.SaveChangesAsync();
            }
            return null;
        }

        public async Task<List<CVEntity>> GetAllCVs()
        {
            var CVs = await db.CVs
                .Include(x => x.ProjectCVList)
                .ThenInclude(x => x.Project)
                .Include(x => x.User)
                .ToListAsync();

            if (CVs != null)
            {
                return CVs;
            }
            return null;
        }

        public async Task<CVEntity> GetCVById(int id)
        {
            var cv = await db.CVs.Include(x => x.User)
                .Include(x => x.ProjectCVList).ThenInclude(x => x.Project)
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == id);

            if (cv != null)
            {
                return cv;
            }
            return null;
        }



        public async Task<List<CVEntity>> GetCVsBySearch(string search)
        {
            try
            {
                return await db.CVs.Include(x => x.User)
                    .Include(x => x.ProjectCVList).ThenInclude(x => x.Project)
                    .Where(cv => cv.CVName.Trim().ToLower().Contains(search))
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        public async Task RemoveAllProjectCVs(int CVId)
        {

            var get = await this.db.ProjectCVs.Where(x => x.CVId == CVId).ToListAsync();

            this.db.ProjectCVs.RemoveRange(get);
            await db.SaveChangesAsync();
        }

        public async Task<CVEntity> UpdateCV(CVEntity cv)
        {
            try
            {
                var findUser = await db.Users.Where(x => x.Id == cv.UserId)
                   .FirstOrDefaultAsync();

                var newModel = new CVEntity
                {
                    CVName = cv.CVName,
                    CreatedAt = cv.CreatedAt,
                    User = findUser,
                    Id = cv.Id
                };
                db.CVs.Update(newModel);
                //await db.SaveChangesAsync();

                var addedCV = await db.CVs.Where(x => x.Id == newModel.Id)
                    .FirstOrDefaultAsync();

                var projectCVList = new List<ProjectCVEntity>();
                var projectCVs = cv.ProjectCVList;
                foreach (var projectCV in projectCVs)
                {
                    var findProject = await db.Projects.Where(x => x.Id == projectCV.ProjectId)
                    .FirstOrDefaultAsync();
                    var newProjectCV = new ProjectCVEntity
                    {
                        CreatedAt = projectCV.CreatedAt,
                        Position = projectCV.Position,
                        Description = projectCV.Description,
                        StartDate = projectCV.StartDate,
                        EndDate = projectCV.EndDate,
                        Project = findProject,
                        CVId = addedCV.Id
                    };
                    projectCVList.Add(newProjectCV);
                }
                await db.ProjectCVs.AddRangeAsync(projectCVList);
                await db.SaveChangesAsync();



                return await GetCVById(newModel.Id);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task Update(CVEntity cv)
        {
            cv.ProjectCVList = null;
            db.CVs.Update(cv);
            await db.SaveChangesAsync();


        }
    }
}