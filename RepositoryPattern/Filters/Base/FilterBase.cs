using System;
using System.Collections.Generic;
using System.Text;

using RepositoryPattern.Filters.HelpFilters;

namespace RepositoryPattern.Filters.Base
{
    public abstract class FilterBase : IFilter
    {
        public abstract Predicate<T> ToListFilter<T>();

        #region Operators
        public static FilterBase operator | (FilterBase firstFilter, FilterBase secondFilter)
        {
            return new OrFilter(firstFilter, secondFilter);
        }

        public static FilterBase operator & (FilterBase firstFilter, FilterBase secondFilter)
        {
            return new AndFilter(firstFilter, secondFilter);
        }

        public static FilterBase operator ! (FilterBase filter)
        {
            return new NotFilter(filter);
        }
        #endregion
    }
}
