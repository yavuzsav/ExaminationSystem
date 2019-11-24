using System;
using System.Collections.Generic;
using System.Linq;

namespace ExaminationSystem.MvcCoreWebUI.HelperMethods
{
    public static class PagingListSelectBySearch
    {
        public static List<T> SelectList<T>(List<T> entityList, List<T> searchedList, int page, int pageSize)
        {
            return searchedList.Count == 0
                ? entityList.Skip((page - 1) * pageSize).Take(pageSize).ToList()
                : searchedList.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }

        public static int GetPageCount<T>(List<T> entityList, List<T> searchedList, int pageSize)
        {
            return (int)Math.Ceiling(searchedList.Count == 0
                ? entityList.Count / (double)pageSize
                : searchedList.Count / (double)pageSize);
        }
    }
}