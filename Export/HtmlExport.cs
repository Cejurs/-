using System;
using System.IO;
using System.Linq;
using System.Text;

namespace Тестовое_задание.Export
{
    // Экспортирует отчет в формат html, можно получить pdf через печать.
    public class HtmlExport : IExport
    {
        StringBuilder htmlBuilder = new StringBuilder();
        public bool Export(Report report, out string message)
        {
            message = string.Empty;
            #region html_head_style
            htmlBuilder.AppendLine("<!DOCTYPE html>");
            htmlBuilder.AppendLine("<head><meta charset=\"UTF-8\">");
            htmlBuilder.AppendLine("<meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">");
            htmlBuilder.AppendLine("<style>");
            htmlBuilder.AppendLine("@page { size: A4;margin: 0;}");
            htmlBuilder.AppendLine("* { box-sizing: border-box;}");
            htmlBuilder.AppendLine("@media screen{\r\n.page::after{\r\nposition: absolute;\r\ncontent: '';\r\n top: 0;\r\nleft: 0;\r\n width: calc(100% - var(--bleeding) * 2);\r\nheight: calc(100% - var(--bleeding) * 2);\r\nmargin: var(--bleeding);\r\noutline: thin dashed black;\r\npointer-events: none;\r\nz-index: 9999;\r\n  }\r\n}");
            htmlBuilder.AppendLine("body {\r\n margin: 0 auto;\r\n padding: 0;\r\n background: rgb(204, 204, 204);\r\n display: flex;\r\n flex-direction: column;\r\n}");
            htmlBuilder.AppendLine(".page {\r\n  display: inline-block;\r\n  position: relative;\r\n  height: 297mm;\r\n  width: 210mm;\r\n  font-size: 12pt;\r\n  margin: 2em auto;\r\n  padding: calc(var(--bleeding) + var(--margin));\r\n  box-shadow: 0 0 0.5cm rgba(0, 0, 0, 0.5);\r\n  background: white;\r\n}");
            htmlBuilder.AppendLine(".header{\r\ntext-align: center\r\n}");
            htmlBuilder.AppendLine("@media print {\r\n  .page {\r\n    margin: 0;\r\n    overflow: hidden;\r\n  }\r\n}");
            htmlBuilder.AppendLine("table {\r\n  border-collapse: collapse;\r\n  margin-left: auto;\r\n  margin-right:auto;\r\n}");
            htmlBuilder.AppendLine("th {\r\n  background: #ccc;\r\n}");
            htmlBuilder.AppendLine("th, td {\r\n  border: 1px solid #ccc;\r\n  padding: 8px;\r\n}");
            htmlBuilder.AppendLine("tr:nth-child(even) {\r\n  background: #efefef;\r\n}\r\n\r\ntr:hover {\r\n  background: #d1d1d1;\r\n}");
            htmlBuilder.AppendLine("</style>\r\n</head>");
            #endregion

            var tableRowCount = 20;
            htmlBuilder.AppendLine("<body style=\"--bleeding: 0.5cm;--margin: 1cm;\">");
            htmlBuilder.AppendLine("<div class=\"page\">");
            htmlBuilder.AppendLine("<h3 class=\"header\">Справка о неисполненных документах и обращениях граждан</h3>");
            htmlBuilder.AppendLine($"<p>Не исполнено в срок <strong>{report.UnfullfilledCount}</strong> документов, из них:</p>");
            htmlBuilder.AppendLine("<ul>");
            htmlBuilder.AppendLine($"<li>количество неисполненных входящих документов: <strong>{report.UnfullfilledDocumentCount}</strong>;</li>");
            htmlBuilder.AppendLine($"<li>количество неисполненных письменных обращений граждан: <strong>{report.UnfullfilledRequestCount}</strong>.</li>");
            htmlBuilder.AppendLine("</ul>");
            htmlBuilder.AppendLine($"<p>Сортировка: {report.SortType}</p>");

            TableHeader();

            var data = report.ReportData.ToList();
            for(var i = 0; i < data.Count; i++)
            {
                var datarow = data[i];
                htmlBuilder.AppendLine($"<tr><td>{i + 1}</td><td>{datarow.ResponsiblePerson}<td>{datarow.UnfullfiledDocumentCount}</td>");
                htmlBuilder.AppendLine($"<td>{datarow.UnfullfilledRequestCount}</td><td>{datarow.SummaryUnfullfiledCount}</td></tr>");
                if (i == tableRowCount)
                {
                    tableRowCount += 25;
                    htmlBuilder.AppendLine("</table>");
                    htmlBuilder.AppendLine("</div>");
                    htmlBuilder.AppendLine("<div class=\"page\">");
                    TableHeader();
                }
            }
            htmlBuilder.AppendLine("</table>");
            htmlBuilder.AppendLine($"<p>Дата составления справки: {report.CreationDate}</p>");
            htmlBuilder.AppendLine("</div>");

            var html = htmlBuilder.ToString();
            var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var fileName = string.Format(@"Report_{0}.html", Guid.NewGuid());
            var fullpath = Path.Combine(path, fileName);
            using (var stream = new FileStream(fullpath, FileMode.Create))
            {
                byte[] buffer = Encoding.UTF8.GetBytes(html);
                stream.Write(buffer, 0, buffer.Length);
            }
            message = $"Данные экспортированы в {fullpath}";
            return true;
        }

        private void TableHeader()
        {
            htmlBuilder.AppendLine("<table>");
            htmlBuilder.AppendLine(" <tr>\r\n<th>№ п.п</th>\r\n<th>Ответственный исполнитель</th>\r\n");
            htmlBuilder.AppendLine("<th>Количество неисполненных входящих документов</th>\r\n<th>Количество неисполненных письменных обращений граждан</th>\r\n");
            htmlBuilder.AppendLine("<th>Общее количество документов и обращений</th></tr>");
        }
    }
}
