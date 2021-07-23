using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Play.Catalog.Service.Models;

namespace Play.Catalog.Service.Repositories
{
    public interface IItemRepository
    {
        Task<IReadOnlyCollection<Item>> GetItemsAsync();
        Task<Item> GetItemAsync (Guid id);
        Task CreateItemAsync(Item item);
        Task UpdateItem(Item item);
        Task DeleteItemAsync(Guid id);
    }
}