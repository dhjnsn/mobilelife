using System.Collections.Generic;

namespace TaxManager.Models
{
    public class MunicipalityRepositoryFacade : IRepositoryFacade<Municipality>
    {
        readonly LiteDB.LiteRepository repository;

        public MunicipalityRepositoryFacade(string connectionString)
        {
            repository = new LiteDB.LiteRepository(connectionString);
        }

        public IEnumerable<Municipality> GetAll()
        {
            return repository.Fetch<Municipality>(LiteDB.Query.All());
        }

        public Municipality GetByName(string name)
        {
            return repository.SingleOrDefault<Municipality>(
                    LiteDB.Query.EQ("Name_lowercase", name.ToLower()));
        }

        public void Upsert(Municipality municipality)
        {
            repository.Upsert(municipality);
        }

        public void ReplaceAll(IEnumerable<Municipality> municipalities)
        {
            using (var transaction = repository.Database.BeginTrans())
            {
                repository.Database.DropCollection("municipality");
                repository.Insert(municipalities);
                transaction.Commit();
            }
        }
        
    }
}