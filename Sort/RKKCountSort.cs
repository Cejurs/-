using System;
using Тестовое_задание.Filters;

namespace Тестовое_задание.Sort
{
    public class RKKCountSort : ISort
    {
        public string Type => "по количеству РКК";

        public Comparison<ReportRow> Comparison => new Comparison<ReportRow>((ReportRow row1,ReportRow row2) =>
        {
            if(row1.UnfullfiledDocumentCount==row2.UnfullfiledDocumentCount)
            {
                return row2.UnfullfilledRequestCount - row1.UnfullfilledRequestCount;
            }

            return row2.UnfullfiledDocumentCount - row1.UnfullfiledDocumentCount;
        });
    }
}
