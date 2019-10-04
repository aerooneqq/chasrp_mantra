using RepositoryPattern.Filters.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryPattern.Filters.HelpFilters
{
    /// <summary>
    /// The filter which is used to preform the OR operator on two other filters.
    /// </summary>
    public class OrFilter : FilterBase
    {
        private readonly FilterBase firstFilter;
        private readonly FilterBase secondFilter;

        public OrFilter(FilterBase firstFilter, FilterBase secondFilter)
        {
            this.firstFilter = firstFilter;
            this.secondFilter = secondFilter;
        }

        public override Predicate<T> ToListFilter<T>()
        {
            return new Predicate<T>(t =>
            {
                return firstFilter.ToListFilter<T>()(t) | secondFilter.ToListFilter<T>()(t);
            });
        }
    }
}
