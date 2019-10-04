using RepositoryPattern.Filters;
using RepositoryPattern.Filters.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryPattern.Filters
{
    /// <summary>
    /// An exmaple of the filter.
    /// This filter is used to find recrods where the value of the given property 
    /// equals the given value.
    /// </summary>
    /// <typeparam name="ValueType">
    /// The property's value type.
    /// </typeparam>
    class EqualityFilter<ValueType> : FilterBase
    {
        private readonly string propertyName;
        private readonly ValueType value;

        public EqualityFilter(string propertyName, ValueType value)
        {
            this.propertyName = propertyName;
            this.value = value;
        }

        public override Predicate<T> ToListFilter<T>()
        {
            return new Predicate<T>(t => typeof(T).GetProperty(propertyName).GetValue(t).Equals(value));
        }
    }
}
