using System.Collections.Generic;
using System.Threading.Tasks;

namespace TodoList.Api.Repositories
{
    public interface IRepository<T>
    {
        Task<List<T>> FindAll();
        Task<T> Find(int id);
        Task Save(T t);
        Task Update(T t);
        Task Delete(int id);
    }
}
