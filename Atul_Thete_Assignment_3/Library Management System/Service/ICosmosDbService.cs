using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library_Management_System.Service
{
    public interface ICosmosDbService
    {
        Task<IEnumerable<T>> GetItemsAsync<T>(string containerName, string queryString);
        Task<T> GetItemAsync<T>(string containerName, string id);
        Task AddItemAsync<T>(string containerName, T item);
        Task UpdateItemAsync<T>(string containerName, string id, T item);
        Task DeleteItemAsync<T>(string containerName, string id);
    }
}
