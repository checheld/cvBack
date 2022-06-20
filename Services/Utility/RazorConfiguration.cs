using Services.Properties;

namespace Services.Utility
{
    public class RazorConfiguration
    {
        public static string myPdf = "myPdf";
      
        static RazorConfiguration()
        {
            myPdf = Resources.myPdf;
           
        }
    }
}
