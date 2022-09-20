using System.Collections.Generic;

namespace ForestInteractiveTestApp.IRepository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IEnumerable<Model> Search<Model>(object parameters, string query);
        IEnumerable<Model> ExecuteQuery<Model>(object parameters, string query);
    }
}
