using RepositoryPattern.Filters.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryPattern.Filters.HelpFilters
{
    /// <summary>
    /// The help filter to perform AND opeartion for two filters.
    /// </summary>
    public class AndFilter : FilterBase
    {
        private readonly FilterBase firstFilter;
        private readonly FilterBase secondFilter;

        public AndFilter(FilterBase firstFilter, FilterBase secondFilter)
        {
            this.firstFilter = firstFilter;
            this.secondFilter = secondFilter;
        }

        public override Predicate<T> ToListFilter<T>()
        {
            return new Predicate<T>(t =>
            {
                return firstFilter.ToListFilter<T>()(t) && secondFilter.ToListFilter<T>()(t);
            });
        }
    }
}
