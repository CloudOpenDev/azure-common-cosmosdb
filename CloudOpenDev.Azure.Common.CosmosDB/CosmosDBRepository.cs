using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;

namespace CloudOpenDev.Azure.Common.CosmosDB
{
    public class CosmosDBRepository<T> : ICosmosDBRepository<T>
    {
        private readonly Container _container;

        public CosmosDBRepository(string accountEndpoint, string authKeyOrResourceToken, string databaseId, string containerId)
        {
            var client = new CosmosClient(accountEndpoint, authKeyOrResourceToken);
            var database = client.GetDatabase(databaseId);
            _container = database.GetContainer(containerId);
        }

        public async Task<T> UpsertItemAsync(T item)
        {
            var result = await _container.UpsertItemAsync<T>(item);

            return result.Resource;
        }

        public async Task<IEnumerable<T>> GetItemsAsync(string query, string continuationToken, QueryRequestOptions queryRequestOptions = null)
        {
            var feedIterator = _container.GetItemQueryIterator<T>(queryText: query, continuationToken: continuationToken, queryRequestOptions);

            var result = new List<T>();

            while (feedIterator.HasMoreResults)
            {
                var response = await feedIterator.ReadNextAsync();
                result.AddRange(response.Resource);
            }

            return result;
        }

        public async Task<bool> DeleteItemAsync(string itemId, string partitionKey)
        {
            var result = await _container.DeleteItemAsync<T>(itemId, new PartitionKey(partitionKey));

            if (result.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                return true;
            }

            return false;
        }
    }
}
