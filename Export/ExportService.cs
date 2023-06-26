using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Тестовое_задание.Export
{
    /// <summary>
    /// Класс инкапсулиюрущий возможности экспорта
    /// </summary>
    public static class ExportService
    {
        public static ICollection<string> GetExportVariants()
        {
            var names = Assembly.GetExecutingAssembly()
                .GetTypes().Where(m => m.IsClass && m.GetInterface("IExport") != null)
                .Select(x=> x.Name).ToList();
            return names;           
        }

        public static bool Export(string exportVariant,Report report,out string message)
        {
            message = "";
            Type? type = Type.GetType("Тестовое_задание.Export." +exportVariant);
            if(type == null)
            {
                message = "Не удалось распознать вариант экспорта";
                return false;
            }
            IExport? export = (IExport)Activator.CreateInstance(type);
            if(export == null)
            {
                message = "Не удалось экспортировать";
                return false;
            }
            var result = export.Export(report,out message);
            return result;
        }
    }
}
