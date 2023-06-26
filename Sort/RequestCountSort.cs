using System;
using Тестовое_задание.Filters;

namespace Тестовое_задание.Sort
{
    public class RequestCountSort : ISort
    {
        public string Type => "по количеству обращений";

        public Comparison<ReportRow> Comparison => new Comparison<ReportRow>((ReportRow row1, ReportRow row2) =>
        {
            if (row1.UnfullfilledRequestCount == row2.UnfullfilledRequestCount)
            {
                return row2.UnfullfiledDocumentCount - row1.UnfullfiledDocumentCount;
            }

            return row2.UnfullfilledRequestCount - row1.UnfullfilledRequestCount;
        });
    }
}
