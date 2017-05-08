using System.Collections.Generic;

namespace TaxManager.Models
{
    public interface IRepositoryFacade<T>
    {
        IEnumerable<T> GetAll();
        void ReplaceAll(IEnumerable<T> ts);
    }
}