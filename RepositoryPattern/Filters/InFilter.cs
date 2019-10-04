using RepositoryPattern.Filters.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryPattern.Filters
{
    /// <summary>
    /// An example of the filter. 
    /// This filter is used to select records, which property's value is in the given collection.
    /// </summary>
    /// <typeparam name="ValueType"></typeparam>
    public class InFilter<ValueType> : IFilter
    {
        private readonly string propertyName;
        private readonly List<ValueType> values;

        public InFilter(string propertyName, List<ValueType> values)
        {
            this.propertyName = propertyName;
            this.values = values;
        }

        public Predicate<T> ToListFilter<T>()
        {
            return new Predicate<T>(t => values.Contains((ValueType)typeof(T).GetProperty(propertyName)
                .GetValue(t)));
        }
    }
}
