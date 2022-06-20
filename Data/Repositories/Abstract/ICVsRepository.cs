using Entities;

namespace Data.Repositories.Abstract
{
    public interface ICVsRepository
    {
        Task<CVEntity> AddCV(CVEntity cv);
        Task<CVEntity> UpdateCV(CVEntity cv);
        Task<CVEntity> GetCVById(int id);
        Task<List<CVEntity>> GetCVsBySearch(string search);
        Task<string> DeleteCVById(int id);
        Task<List<CVEntity>> GetAllCVs();

        Task RemoveAllProjectCVs(int userId);

        /*Task Update(CVEntity cv);*/
    }
}