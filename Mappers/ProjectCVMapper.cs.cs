using Domain;
using Entities;

namespace Mappers
{
    public static class ProjectCVMapper
    {
        public static ProjectCVEntity ToEntity(ProjectCVDTO projectCV)
        {
            ProjectCVEntity projectCVEntity = new ProjectCVEntity();
            if (projectCV != null)
            {
                projectCVEntity.ProjectId = projectCV.ProjectId;
                projectCVEntity.Project = ProjectMapper.ToEntity(projectCV.Project);
                projectCVEntity.Position = projectCV.Position;
                projectCVEntity.Description = projectCV.Description;

                projectCVEntity.StartDate = projectCV.StartDate;
                projectCVEntity.EndDate = projectCV.EndDate;
                projectCVEntity.Id = projectCV.Id;
                projectCVEntity.CreatedAt = DateTime.Now;
                projectCVEntity.CVId = projectCV.CVId;
                return projectCVEntity;
            }
            return null;
        }

        public static ProjectCVDTO ToDomain(ProjectCVEntity projectCVEntity)
        {
            ProjectCVDTO projectCV = new ProjectCVDTO();
            if (projectCVEntity != null)
            {
                projectCV.Project = ProjectMapper.ToDomain(projectCVEntity.Project);
                projectCV.ProjectId = projectCVEntity.ProjectId;
                projectCV.Position = projectCVEntity.Position;
                projectCV.Description = projectCVEntity.Description;
                projectCV.StartDate = projectCVEntity.StartDate;
                projectCV.EndDate = projectCVEntity.EndDate;
                projectCV.Id = projectCVEntity.Id;
                projectCV.CreatedAt = projectCVEntity.CreatedAt;

                return projectCV;
            }
            return null;
        }

        public static List<ProjectCVDTO> ToDomainList(List<ProjectCVEntity> projectCVEntity)
        {
            List<ProjectCVDTO> projectCVs = new List<ProjectCVDTO>();
            foreach (ProjectCVEntity projectCV in projectCVEntity)
            {
                projectCVs.Add(ToDomain(projectCV));
            }
            return projectCVs;
        }

        public static List<ProjectCVEntity> ToEntityList(List<ProjectCVDTO> projectCV)
        {
            List<ProjectCVEntity> projectCVs = new List<ProjectCVEntity>();
            foreach (ProjectCVDTO e in projectCV)
            {
                projectCVs.Add(ToEntity(e));
            }
            return projectCVs;
        }
    }
}