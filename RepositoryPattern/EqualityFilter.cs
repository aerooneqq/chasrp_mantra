using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryPattern
{
    class EqualityFilter<ValueType> : IFilter
    {
        string propertyName;
        private ValueType value;

        public EqualityFilter(string propertyName, ValueType value)
        {
            this.propertyName = propertyName;
            this.value = value;
        }

        public Predicate<T> ToListFilter<T>()
        {
            return new Predicate<T>(t => typeof(T).GetProperty(propertyName).GetValue(t).Equals(value));
        }
    }
}
