using Domain;

namespace Services.Abstract
{
    public interface ICVsService
    {
        Task<CVDTO> AddCV(CVDTO cv);
        Task<string> DeleteCVById(int id);
        Task<CVDTO> GetCVById(int id);
        Task<List<CVDTO>> GetCVsBySearch(string search);
        Task<CVDTO> UpdateCV(CVDTO cv);
        Task<List<CVDTO>> GetAllCVs();
    }
}
