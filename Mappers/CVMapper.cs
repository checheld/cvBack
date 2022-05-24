using Domain;
using Entities;

namespace Mappers
{
    public static class CVMapper
    {
        public static CVEntity ToEntity(CVDTO cv)
        {
            CVEntity cvEntity = new CVEntity();
            if (cv != null)
            {
                cvEntity.ProjectCVList = ProjectCVMapper.ToEntityList(cv.ProjectCVList);
                cvEntity.CVName = cv.CVName;
                cvEntity.UserId = cv.UserId;
                cvEntity.User = UserMapper.ToEntity(cv.User);

                cvEntity.Id = cv.Id;
                cvEntity.CreatedAt = DateTime.Now;

                return cvEntity;
            }
            return null;
        }

        public static CVDTO ToDomain(CVEntity cvEntity)
        {
            CVDTO cv = new CVDTO();
            if (cvEntity != null)
            {
                cv.ProjectCVList = ProjectCVMapper.ToDomainList(cvEntity.ProjectCVList);
                cv.CVName = cvEntity.CVName;
                cv.User = UserMapper.ToDomain(cvEntity.User);
                cv.UserId = cvEntity.UserId;
                cv.Id = cvEntity.Id;
                cv.CreatedAt = DateTime.Now;

                return cv;
            }
            return null;
        }

        public static List<CVDTO> ToDomainList(List<CVEntity> cvEntity)
        {
            List<CVDTO> CVs = new List<CVDTO>();
            foreach (CVEntity cv in cvEntity)
            {
                CVs.Add(ToDomain(cv));
            }
            return CVs;
        }

        public static List<CVEntity> ToEntityList(List<CVDTO> cv)
        {
            List<CVEntity> CVs = new List<CVEntity>();
            foreach (CVDTO e in cv)
            {
                CVs.Add(ToEntity(e));
            }
            return CVs;
        }
    }
}