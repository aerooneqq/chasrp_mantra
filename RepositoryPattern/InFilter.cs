using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryPattern
{
    class InFilter<ValueType> : IFilter
    {
        private string propertyName;
        private List<ValueType> values;

        public InFilter(string propertyName, List<ValueType> values)
        {
            this.propertyName = propertyName;
            this.values = values;
        }

        public Predicate<T> ToListFilter<T>()
        {
            return new Predicate<T>(t => values.Contains((ValueType)typeof(T).GetProperty(propertyName).GetValue(t)));
        }
    }
}
