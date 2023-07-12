using LoadDataAPI.Models;
using LoadDataAPI.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace LoadDataAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FactoryLoadController : ControllerBase
    {
        private readonly FactoryLoadService _factoryLoadService;

        public FactoryLoadController(FactoryLoadService factoryLoadService)
        {
            _factoryLoadService = factoryLoadService;
        }

        [HttpGet]
        public async Task<List<MongoFactoryLoad>> Get()
        {
            return await _factoryLoadService.GetAsync();
        }

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<MongoFactoryLoad>> Get(ObjectId id)
        {
            var factoryLoad = await _factoryLoadService.GetAsync(id);

            if (factoryLoad is null)
                return NotFound();

            return factoryLoad;
        }

        [HttpPost]
        public async Task<IActionResult> Post(MongoFactoryLoad newFactoryLoad)
        {
            await _factoryLoadService.CreateAsync(newFactoryLoad);

            return CreatedAtAction(nameof(Get), new { id = newFactoryLoad.Id }, newFactoryLoad);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(ObjectId id, MongoFactoryLoad updatedFactoryLoad)
        {
            var factoryLoad = await _factoryLoadService.GetAsync(id);

            if (factoryLoad is null)
                return NotFound();

            updatedFactoryLoad.Id = factoryLoad.Id;

            await _factoryLoadService.UpdateAsync(id, updatedFactoryLoad);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(ObjectId id)
        {
            var factoryLoad = await _factoryLoadService.GetAsync(id);

            if (factoryLoad is null)
                return NotFound();

            await _factoryLoadService.RemoveAsync(id);

            return NoContent();
        }

    }
}

