using System;
using System.ComponentModel.DataAnnotations;

namespace Play.Catalog.Service
{
    public record ItemDto (Guid Id, string Name, string Description, decimal Price, DateTimeOffset CreatedAt);
    public record CreateItemDto ([Required] string Name, [Required] string Description, [Range(0, 1000)] decimal Price);
    public record UpdateItemDto ([Required] string Name, [Required] string Description, [Range(0, 1000)]  decimal Price);
    
}