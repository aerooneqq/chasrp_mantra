using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace RepositoryPattern
{
    class Database : IRepository<Model>
    {
        private List<Model> records;

        public Database()
        {
            records = new List<Model>();
        }

        public void Delete(IFilter filter)
        {
            records.RemoveAll(filter.ToListFilter<Model>());
        }

        public List<Model> Get(IFilter filter)
        {
            return records.FindAll(filter.ToListFilter<Model>());
        }

        public void Insert(Model entity)
        {
            records.Add(entity);
        }

        public void Update(Model entity)
        {
            records[records.FindIndex(e => e.ID == entity.ID)] = entity;
        }

        public void Dispose()
        {
            records = null;
        }
    }
}
