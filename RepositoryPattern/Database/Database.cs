using System.Collections.Generic;

using RepositoryPattern.Filters.Base;

namespace RepositoryPattern.Database
{
    /// <summary>
    /// The concrete implementation of the IRepository. 
    /// In this demo implementation i use list as a storage, but in real life it can be any
    /// database.
    /// In this database we use filters to select and delete entities.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class Database<T> : IRepository<T> where T : UniqueEntity
    {
        private List<T> records;

        public Database()
        {
            records = new List<T>();
        }

        public void Delete(IFilter filter)
        {
            records.RemoveAll(filter.ToListFilter<T>());
        }

        public List<T> Get(IFilter filter)
        {
            return records.FindAll(filter.ToListFilter<T>());
        }

        public void Insert(T entity)
        {
            records.Add(entity);
        }

        public void Update(T entity)
        {
            records[records.FindIndex(e => e.ID == entity.ID)] = entity;
        }

        public void Print()
        {
            foreach (T obj in records)
            {
                System.Console.WriteLine(obj);
            }

            System.Console.WriteLine();
        }

        public void Dispose()
        {
            records = null;
        }
    }
}
