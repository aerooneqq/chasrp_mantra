using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryPattern
{
    abstract class FilterBase : IFilter
    {
        public abstract Predicate<T> ToListFilter<T>();

        public static FilterBase operator | (FilterBase filterBase, FilterBase filterBase1)
        {
            throw new NotImplementedException();
        }

        public static FilterBase operator & (FilterBase filterBase, FilterBase filterBase1)
        {
            throw new NotImplementedException();
        }
    }
}
