using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
        public ActionResult<ItemDto> CreateItem(CreateItemDto item)
        {
            var (name, description, price) = item;
            var newItem = new ItemDto(Guid.NewGuid(), name, description, price, DateTimeOffset.Now);
            Items.Add(newItem);
            return CreatedAtAction(nameof(GetItem), new {id = newItem.Id}, newItem);
        }

        //Method: PUT
        //Route: /items/{id}
        //Return: Void
        [HttpPut("{id:guid}")]
        public IActionResult UpdateItem(Guid id, UpdateItemDto update)
        {
            var indexOfItem = Items.FindIndex(element => element.Id == id);
            if (indexOfItem == -1) return NotFound();
            var (name, description, price) = update;
            var updatedItem = Items[indexOfItem] with
            {
                Name = name,
                Description = description,
                Price = price
            };
            Items[indexOfItem] = updatedItem;
            return NoContent();
        }

        //Method: DELETE   
        //Route: /items/{id}
        //Return: void
        [HttpDelete("{id:guid}")]
        public IActionResult DeleteItem(Guid id)
        {
            var index = Items.FindIndex(item => item.Id == id);
            if (index == -1) return NotFound();
            Items.RemoveAt(index);
            return NoContent();
        }
    }
}