﻿using System;
using Тестовое_задание.Filters;

namespace Тестовое_задание.Sort
{
    public class ResponsiblePersonSort : ISort
    {
        public string Type => "по отвественному лицу";

        public Comparison<ReportRow> Comparison => new Comparison<ReportRow>((ReportRow row, ReportRow row2) =>
        {
            return string.Compare(row.ResponsiblePerson, row2.ResponsiblePerson);
        });
    }
}
