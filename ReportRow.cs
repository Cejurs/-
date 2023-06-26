using System;

namespace Тестовое_задание
{
    // Струтурная единица отчета - строка в табличке
    public class ReportRow
    {
        // Ответственное лицо
        public string ResponsiblePerson { get; private set; }
        // Количество неисполненых документов
        public int UnfullfiledDocumentCount { get; private set; }
        // Количество неисполненных запросов граждан
        public int UnfullfilledRequestCount { get; private set; }
        // Общее количество неисполненых запросов и документов.
        public int SummaryUnfullfiledCount { get => UnfullfiledDocumentCount+UnfullfilledRequestCount; }

        public ReportRow(string responsiblePerson,int unfullfiledDocumentCount,int unfullfilledRequestCount)
        {
            if (string.IsNullOrEmpty(responsiblePerson)) throw new ArgumentException("Некорректное имя ответсвенного лица");

            ResponsiblePerson = responsiblePerson;
            UnfullfiledDocumentCount = unfullfiledDocumentCount;
            UnfullfilledRequestCount = unfullfilledRequestCount;
        }

        public void IncreaseUnfunfilledDocumentCount()
        {
            UnfullfiledDocumentCount += 1;
        }

        public void IncreaseUnfunfilledRequestCount()
        {
            UnfullfilledRequestCount += 1;
        }
    }
}
