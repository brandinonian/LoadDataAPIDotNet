using LoadDataAPI.Models;
using LoadDataAPI.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace LoadDataAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomLoadController : ControllerBase
    {
        private readonly CustomLoadService _customLoadService;

        public CustomLoadController(CustomLoadService customLoadService)
        {
            _customLoadService = customLoadService;
        }

        [HttpGet]
        public async Task<List<MongoCustomLoad>> Get()
        {
            return await _customLoadService.GetAsync();
        }

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<MongoCustomLoad>> Get(ObjectId id)
        {
            var factoryLoad = await _customLoadService.GetAsync(id);

            if (factoryLoad is null)
                return NotFound();

            return factoryLoad;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] MongoCustomLoad newCustomLoad)
        {
            await _customLoadService.CreateAsync(newCustomLoad);

            return CreatedAtAction(nameof(Get), new { id = newCustomLoad.Id }, newCustomLoad);
        }

        /*
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(ObjectId id, MongoCustomLoad updatedCustomLoad)
        {
            var customLoad = await _customLoadService.GetAsync(id);

            if (customLoad is null)
                return NotFound();

            updatedCustomLoad.Id = customLoad.Id;

            await _customLoadService.UpdateAsync(id, updatedCustomLoad);

            return NoContent();
        }
        */

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(ObjectId id)
        {
            await _customLoadService.RemoveAsync(id);

            return NoContent();
        }
    }
}

