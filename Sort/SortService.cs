using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Тестовое_задание.Filters;

namespace Тестовое_задание.Sort
{
    public static class SortService
    {
        // Пока пусть сортировки хранятся в памяти их не слишком много.
        public static List<ISort> Sorts = GetSorts();
        public static ISort DefaultSort = new ResponsiblePersonSort();

        private static List<ISort> GetSorts()
        {
            var sortsTypes = Assembly.GetExecutingAssembly()
                .GetTypes().Where(m => m.IsClass && m.GetInterface("ISort") != null)
                .Select(x=> (ISort)Activator.CreateInstance(x))
                .ToList();
            return sortsTypes;
            
        }

        public static ISort GetSort(string sortVariant)
        {
            return Sorts.Where(x => x.Type == sortVariant).FirstOrDefault() ?? DefaultSort;
        }

        public static List<string> GetSortVariants()
        {
            var a= Sorts.Select(x => x.Type).ToList();
            return Sorts.Select(x => x.Type).ToList();
        }
    }
}
