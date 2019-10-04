using RepositoryPattern.Filters.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryPattern.Filters.HelpFilters
{
    /// <summary>
    /// The filter which is used to perform NOT operation on a filter.
    /// </summary>
    public class NotFilter : FilterBase
    {
        private readonly FilterBase filter;

        public NotFilter(FilterBase filter)
        {
            this.filter = filter;
        }

        public override Predicate<T> ToListFilter<T>()
        {
            return new Predicate<T>(t =>
            {
                return !(filter.ToListFilter<T>()(t));
            });
        }
    }
}
