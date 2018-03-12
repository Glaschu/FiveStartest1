using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using FiveStartest1.Models;
using System.Linq;
namespace FiveStartest1.Controllers
{
    [Route("api/[controller]")]
    public class GameController : Controller
    {
        private readonly GameContext _context;

        public GameController(GameContext context)
        {
            _context = context;

            if (_context.GameItems.Count() == 0)
            {
                _context.GameItems.Add(new GameItem { Name = "Item1" });
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public IEnumerable<GameItem> GetAll()
        {
            return _context.GameItems.ToList();
        }

        [HttpGet("{id}", Name = "GetGame")]
        public IActionResult GetById(long id)
        {
            var item = _context.GameItems.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }
        [HttpPost]
        public IActionResult Create([FromBody] GameItem item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            _context.GameItems.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetGame", new { id = item.Id }, item);
        }
        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] TodoItem item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }

            var todo = _context.TodoItems.FirstOrDefault(t => t.Id == id);
            if (todo == null)
            {
                return NotFound();
            }

            todo.IsComplete = item.IsComplete;
            todo.Name = item.Name;

            _context.TodoItems.Update(todo);
            _context.SaveChanges();
            return new NoContentResult();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var todo = _context.TodoItems.FirstOrDefault(t => t.Id == id);
            if (todo == null)
            {
                return NotFound();
            }

            _context.TodoItems.Remove(todo);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }
}