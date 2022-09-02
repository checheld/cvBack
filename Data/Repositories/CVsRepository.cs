#region Imports
using Data.Repositories.Abstract;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
#endregion

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
                await db.CVs.AddAsync(cv);
                await db.SaveChangesAsync();

                var addedCV = await db.CVs.Where(x => x.CreatedAt == cv.CreatedAt).FirstOrDefaultAsync();

                return await GetCVById(addedCV.Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task AddProjectCVs(List<ProjectCVEntity> projectCV)
        {
            try
            {
                await db.ProjectCVs.AddRangeAsync(projectCV);
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> DeleteCVById(int id)
        {
            try
            {
                db.CVs.Remove(await db.CVs.SingleOrDefaultAsync(x => x.Id == id));
                await db.SaveChangesAsync();

                return id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<CVEntity>> GetAllCVs()
        {
            try
            {
                var CVs = await db.CVs.Include(x => x.ProjectCVList).ThenInclude(x => x.Project)
                    .Include(x => x.User).ToListAsync();

                return CVs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<CVEntity> GetCVById(int id)
        {
            try
            {
                var cv = await db.CVs.Include(x => x.User).Include(x => x.ProjectCVList)
               .ThenInclude(x => x.Project).ThenInclude(x => x.TechnologyList)
               .AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);

                return cv;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ProjectCVEntity> GetProjectCVById(int id)
        {
            try
            {
                return await db.ProjectCVs.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<CVEntity>> GetCVsBySearch(string search)
        {
            try
            {
                return await db.CVs.Include(x => x.User).Include(x => x.ProjectCVList).ThenInclude(x => x.Project)
                    .Where(cv => cv.CVName.Trim().ToLower().Contains(search)).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task DeleteProjectCV(int projectCVId)
        {
            try
            {
                db.ProjectCVs.Remove(await db.ProjectCVs.SingleOrDefaultAsync(x => x.Id == projectCVId));
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task RemoveAllProjectCVs(int CVId)
        {
            try
            {
                this.db.ProjectCVs.RemoveRange(await this.db.ProjectCVs.Where(x => x.CVId == CVId).ToListAsync());
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ProjectCVEntity> UpdateProjectCV(ProjectCVEntity projectCV)
        {
            try
            {
                db.ProjectCVs.Update(projectCV);
                await db.SaveChangesAsync();

                return projectCV;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<CVEntity> UpdateCV(CVEntity cv)
        {
            try
            {
                db.Entry(cv).State = EntityState.Modified;
                await db.SaveChangesAsync();
                db.Entry(cv).State = EntityState.Detached;

                return await GetCVById(cv.Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}