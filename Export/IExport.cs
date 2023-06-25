namespace Тестовое_задание.Export
{
    interface IExport
    {
        public bool Export(Report report, out string message);
    }
}
