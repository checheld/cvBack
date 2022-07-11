using Domain;
using System.Text;

namespace Services.Utility
{
    public class TemplatePdfGenerator
    {
        public static string GetHTMLString(CVDTO CV)
        {
            var sb = new StringBuilder();
            sb.Append(@"
                        <html>
                            <head>
                            </head>
                            <body>
<table>
<p>!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!</p>
");
                sb.Append(@"
                                </table>
                            </body>
                        </html>");
                return sb.ToString();
        }
    }
}
