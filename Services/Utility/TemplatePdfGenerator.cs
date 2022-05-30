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
                            <body>");
                sb.AppendFormat($@" <h1>{{CV.User.FirstName}} {{CV.User.LastName}}</h1>
                                    <h2>Проекты</h2>");
            foreach (var project in CV.ProjectCVList)
            {
                sb.AppendFormat($@"< div>
                                    <h3>{{project.Project.Name}}</h3>
                                    <p>{{project.Position}}</p>
                                    <p>{{project.StartDate}} - {{project.EndDate}}</p>
                                    <p>{{project.Dedscription}}</p>"
                                );
            }
                sb.Append(@"
                                </table>
                            </body>
                        </html>");
                return sb.ToString();
        }
    }
}
