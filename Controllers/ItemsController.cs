using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Play.Catalog.Service.Models;
using Play.Catalog.Service.Repositories;

namespace Play.Catalog.Service.Controllers
{
    [ApiController]
    [Route("items")]
    public class ItemsController : ControllerBase
    {
        private readonly ItemRepository _repository = new();


        //Method: GET 
        //Route: /items
        //Returns: All items
        [HttpGet]
        public async Task<IEnumerable<ItemDto>> GetItems()
        {
            return (await _repository.GetItemsAsync()).Select(item => item.AsItemDto());
        }

        //Method: GET 
        //Route: /items/{id}
        //Returns: Items with Id equal to id parameter
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ItemDto>> GetItem(Guid id)
        {
            var item = await _repository.GetItemAsync(id);
            return item == null ? NotFound() : item.AsItemDto();
        }

        //Method: POST
        //Route: /items
        //Return: Created item
        [HttpPost]
        public async Task<ActionResult<ItemDto>> CreateItem(CreateItemDto item)
        {
            var (name, description, price) = item;
            var newItem = new Item
            {
                Id = Guid.NewGuid(), Name = name, Description = description, Price = price,
                CreatedAt = DateTimeOffset.Now
            };
            await _repository.CreateItemAsync(newItem);
            return CreatedAtAction(nameof(GetItem), new {id = newItem.Id}, newItem);
        }

        //Method: PUT
        //Route: /items/{id}
        //Return: Void
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateItem(Guid id, UpdateItemDto update)
        {
            var searchItem = await _repository.GetItemAsync(id);
            if (searchItem == null) return NotFound();

            var (name, description, price) = update;
            var updatedItem = new Item
            {
                Id = searchItem.Id,
                Name = name,
                Description = description,
                Price = price,
                CreatedAt = searchItem.CreatedAt
            };
            await _repository.UpdateItem(updatedItem);
            return NoContent();
        }

        //Method: DELETE   
        //Route: /items/{id}
        //Return: void
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteItem(Guid id)
        {
            var searchItem = await _repository.GetItemAsync(id);
            if (searchItem == null ) return NotFound();
            await _repository.DeleteItemAsync(id);
            return NoContent();
        }
    }
}