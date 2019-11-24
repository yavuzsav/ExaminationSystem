using System;
using System.Collections.Generic;
using System.Linq;

namespace ExaminationSystem.Framework.Extensions
{
    public static class DuplicationExtensions
    {
        //public static bool IsDuplicate<TSource, TKey>(this IQueryable<TSource> sources,
        //    Expression<Func<TSource, TKey>> selector, TKey value)
        //{
        //    return sources.Select(selector).Contains(value);
        //}

        //public static bool IsDuplicate<TSource>(this IQueryable<TSource> sources,
        //    Expression<Func<TSource, bool>> filter1, Expression<Func<TSource, bool>> filter2)
        //{
        //    return sources.Where(filter1).Where(filter2).Any();
        //}

        public static bool IsDuplicate<TSource>(this IList<TSource> sources, params Func<TSource, bool>[] selectors)
        {
            //return sources.Any(selector);
            foreach (var selector in selectors)
            {
                if (sources.Any(selector))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool IsMultiDuplicate<TSource>(this IList<TSource> sources, params Func<TSource, bool>[] filters)
        {
            var list = sources;

            foreach (var filter in filters)
            {
                list = list.Where(filter).ToList();
            }

            return list.Any();
        }
    }
}