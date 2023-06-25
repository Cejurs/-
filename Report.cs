using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace Тестовое_задание
{
    public class Report
    {
        private List<ReportRow> rows;
        public DateTime CreationDate { get;}

        public ICollection<ReportRow> ReportData => rows;
        public int UnfullfilledDocumentCount {  get => rows.Sum(x=>x.UnfullfiledDocumentCount) ;}
        public int UnfullfilledRequestCount { get => rows.Sum(x => x.UnfullfilledRequestCount); }
        public int UnfullfilledCount { get => UnfullfilledDocumentCount + UnfullfilledRequestCount; }

        public Report(ICollection<ReportRow> rows) 
        {
            // Тут можно не выкидывать ошибку, а создавать пустой отчет, если это необходимо
            if (rows == null || rows.Count == 0) throw new ArgumentNullException();
            this.rows = rows.ToList();
            CreationDate = DateTime.Now;
        }

        private void Filter()
        {
            rows.OrderBy(x=> x.ResponsiblePerson).ToList();
        }
    }
}
