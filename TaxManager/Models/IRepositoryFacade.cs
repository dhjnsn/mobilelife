using System.Collections.Generic;

namespace TaxManager.Models
{
    public interface IRepositoryFacade<T>
    {
        IEnumerable<T> GetAll();
        T GetByName(string name);
        void Upsert(T t);
        void ReplaceAll(IEnumerable<T> ts);
    }
}