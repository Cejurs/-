namespace Тестовое_задание.Export
{
    /// <summary>
    /// Интерфейс для добавления новых возможностей экспорта
    /// </summary>
    interface IExport
    {
        public bool Export(Report report, out string message);
    }
}
