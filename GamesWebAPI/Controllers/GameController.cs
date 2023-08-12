using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using GamesWebAPI.Data;
using GamesWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GamesWebAPI.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase {
        private readonly GameDbContext _dbContext;
        public GameController(GameDbContext _dbContext) => this._dbContext = _dbContext;


        [HttpGet]
        public async Task<IEnumerable<Game>> GetGames() =>
            await _dbContext.Games.ToListAsync();



        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Game), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByIdResult(int id) {
            var game = await _dbContext.Games.FindAsync(id);
            return game == null ? NotFound() : Ok(game);
        }



        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreatResult(Game game) {
            await _dbContext.Games.AddAsync(game);
            await _dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetByIdResult), new { id = game.Id }, game);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Update(int id, Game game) {
            if (id != game.Id) return BadRequest();

            _dbContext.Update(game).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id) {
            var game = await _dbContext.Games.FindAsync(id);
            if (game == null) return NotFound();
            _dbContext.Games.Remove(game);
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }
    }



}
