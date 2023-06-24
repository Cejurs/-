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

        public Report(ICollection<ReportRow> rows) 
        {
            // Тут можно не выкидывать ошибку, а создавать пустой отчет, если это необходимо
            if (rows == null || rows.Count == 0) throw new ArgumentNullException();
            this.rows = rows.ToList();
        }

        private void Filter()
        {
            rows.OrderBy(x=> x.ResponsiblePerson).ToList();
        }
    }
}
