using Microsoft.Azure.Cosmos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library_Management_System.Service
{
    public class CosmosDbService : ICosmosDbService
    {
        private readonly CosmosClient _cosmosClient;
        private readonly string _databaseName;
        private readonly Dictionary<string, Container> _containers;

        public CosmosDbService(CosmosClient cosmosClient, string databaseName, Dictionary<string, string> containerNames)
        {
            _cosmosClient = cosmosClient;
            _databaseName = databaseName;
            _containers = containerNames.ToDictionary(c => c.Key, c => _cosmosClient.GetContainer(_databaseName, c.Value));
        }

        private Container GetContainer(string containerName)
        {
            return _containers[containerName];
        }

        public async Task AddItemAsync<T>(string containerName, T item)
        {
            var container = GetContainer(containerName);
            await container.CreateItemAsync(item, new PartitionKey(((dynamic)item).UId));
        }

        public async Task DeleteItemAsync<T>(string containerName, string id)
        {
            var container = GetContainer(containerName);
            await container.DeleteItemAsync<T>(id, new PartitionKey(id));
        }

        public async Task<T> GetItemAsync<T>(string containerName, string id)
        {
            try
            {
                var container = GetContainer(containerName);
                ItemResponse<T> response = await container.ReadItemAsync<T>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return default(T);
            }
        }

        public async Task<IEnumerable<T>> GetItemsAsync<T>(string containerName, string queryString)
        {
            var container = GetContainer(containerName);
            var query = container.GetItemQueryIterator<T>(new QueryDefinition(queryString));
            List<T> results = new List<T>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;
        }

        public async Task UpdateItemAsync<T>(string containerName, string id, T item)
        {
            var container = GetContainer(containerName);
            await container.UpsertItemAsync(item, new PartitionKey(id));
        }
    }
}
