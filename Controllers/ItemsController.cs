using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace Play.Catalog.Service.Controllers
{
    [ApiController]
    [Route("items")]
    public class ItemsController : ControllerBase
    {
        private static readonly List<ItemDto> Items = new()
        {
            new ItemDto(Guid.NewGuid(), "Imac 2017", "Work device", 9000, DateTimeOffset.Now),
            new ItemDto(Guid.NewGuid(), "Imac 2017", "Work device", 9000, DateTimeOffset.Now),
            new ItemDto(Guid.NewGuid(), "Imac 2017", "Work device", 9000, DateTimeOffset.Now),
        };

        //Method: GET 
        //Route: /items
        //Returns: All items
        [HttpGet]
        public IEnumerable<ItemDto> GetItems()
        {
            return Items;
        }

        //Method: GET 
        //Route: /items/{id}
        //Returns: Items with Id equal to id parameter
        [HttpGet("{id:guid}")]
        public ActionResult<ItemDto> GetItem(Guid id)
        {
            var item = Items.Find(element => element.Id == id);
            return  item == null ? NotFound() : item;
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