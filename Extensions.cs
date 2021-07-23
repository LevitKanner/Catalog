using Play.Catalog.Service.Models;

namespace Play.Catalog.Service
{
    public static class Extensions
    {
        public static ItemDto AsItemDto(this Item item)
        {
            return new(item.Id, item.Name, item.Description, item.Price, item.CreatedAt);
        }
    }
}