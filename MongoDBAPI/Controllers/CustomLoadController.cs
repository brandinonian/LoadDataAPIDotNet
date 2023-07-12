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
        public async Task<ActionResult<MongoCustomLoad>> Get(ObjectId Id)
        {
            var factoryLoad = await _customLoadService.GetAsync(Id);

            if (factoryLoad is null)
                return NotFound();

            return factoryLoad;
        }

        [HttpPost]
        public async Task<IActionResult> Post(MongoCustomLoad newCustomLoad)
        {
            await _customLoadService.CreateAsync(newCustomLoad);

            return CreatedAtAction(nameof(Get), new { id = newCustomLoad.Id }, newCustomLoad);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(ObjectId Id, MongoCustomLoad updatedCustomLoad)
        {
            var customLoad = await _customLoadService.GetAsync(Id);

            if (customLoad is null)
                return NotFound();

            updatedCustomLoad.Id = customLoad.Id;

            await _customLoadService.UpdateAsync(Id, updatedCustomLoad);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(ObjectId Id)
        {
            var customLoad = await _customLoadService.GetAsync(Id);

            if (customLoad is null)
                return NotFound();

            await _customLoadService.RemoveAsync(Id);

            return NoContent();
        }
    }
}

