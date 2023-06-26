using System;

namespace Тестовое_задание.Filters
{
    // Интерфейс для сортировок
    public interface ISort
    {
        // Указывает, по какому признаку сортируется.
        public string Type { get; }
        public Comparison<ReportRow> Comparison { get; }
    }
}
