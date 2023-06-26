using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Тестовое_задание.Filters
{
    public interface ISort
    {
        // Указывает, по какому признаку сортируется.
        public string Type { get; }
        public Comparison<ReportRow> Comparison { get; }
    }
}
