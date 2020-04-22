using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;

namespace CloudOpenDev.Azure.Common.CosmosDB
{
    public interface ICosmosDBRepository<T>
    {
        Task<IEnumerable<T>> GetItemsAsync(string query, string continuationToken, QueryRequestOptions queryRequestOptions = null);

        Task<T> UpsertItemAsync(T item);

        Task<bool> DeleteItemAsync(string itemId, string partitionKey);
    }
}
