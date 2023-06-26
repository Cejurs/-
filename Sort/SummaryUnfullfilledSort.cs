using System;
using Тестовое_задание.Filters;

namespace Тестовое_задание.Sort
{
    class SummaryUnfullfilledSort : ISort
    {
        public string Type => "по общему числу обращений";

        public Comparison<ReportRow> Comparison => new Comparison<ReportRow>((ReportRow row1,ReportRow row2) =>
        {
            if (row1.SummaryUnfullfiledCount == row2.SummaryUnfullfiledCount)
            {
                return row2.UnfullfiledDocumentCount - row1.UnfullfiledDocumentCount;
            }

            return row2.SummaryUnfullfiledCount - row1.SummaryUnfullfiledCount;
        });
    }
}
