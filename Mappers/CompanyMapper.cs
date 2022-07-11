using Entities;

namespace Mappers
{
    public static class CompanyMapper
    {
        public static CompanyEntity ToEntity(CompanyDTO company)
        {
            CompanyEntity companyEntity = new CompanyEntity();
            if (company != null)
            {
                companyEntity.Name = company.Name;
                companyEntity.Id = company.Id;
                companyEntity.CreatedAt = DateTime.Now;

                return companyEntity;
            }
            return null;
        }

        public static CompanyDTO ToDomain(CompanyEntity companyEntity)
        {
            CompanyDTO company = new CompanyDTO();
            if (companyEntity != null)
            {
                company.Name = companyEntity.Name;
                company.CreatedAt = companyEntity.CreatedAt;
                company.Id = companyEntity.Id;

                return company;
            }
            return null;
        }

        public static List<CompanyDTO> ToDomainList(List<CompanyEntity> companyEntity)
        {
            List<CompanyDTO> companies = new List<CompanyDTO>();
            foreach (CompanyEntity comp in companyEntity)
            {
                companies.Add(ToDomain(comp));
            }
            return companies;
        }
    }
}

