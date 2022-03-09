using Domain;
using Entities;

namespace Mappers
{
    public static class CompanyMapper
    {
        public static CompanyEntity ToEntity(Company company)
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

        public static Company ToDomain(CompanyEntity companyEntity)
        {
            Company company = new Company();

            if (companyEntity != null)
            {
                company.Name = companyEntity.Name;
                company.CreatedAt = companyEntity.CreatedAt;
                company.Id = companyEntity.Id;

                return company;
            }
            return null;
        }

    }
}
