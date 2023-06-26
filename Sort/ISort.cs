using System;

namespace Тестовое_задание.Filters
{
    /// <summary>
    /// Интерфейс для сортировок
    /// </summary>
    public interface ISort
    {
        // Указывает, по какому признаку сортируется.
        public string Type { get; }
        public Comparison<ReportRow> Comparison { get; }
    }
}
