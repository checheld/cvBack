

using PdfSharpCore.Pdf;

namespace Services.Abstract
{
    public interface IPdfService
    {
        Task<byte[]> GetPdf(int id);
    }
}