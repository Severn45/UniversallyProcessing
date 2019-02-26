using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniversallyProcessing.Api.Controllers.Base;
using UniversallyProcessing.Api.Managers;

namespace UniversallyProcessing.Api.Controllers
{
    [Route("api/car")]
    [ApiController]
    public class CarController : BaseController
    {
        private readonly CarManager carManager;

        public CarController(CarManager carManager)
        {
            this.carManager = carManager;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <param name="id">id пользователя</param>
        [HttpGet("{getcarmakes}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<JsonResult> GetCarMakes(int id)
        {
            var carMakes = await carManager.GetCarMakes();
            return Json(carMakes);
        }

        [HttpGet("{fillcarmakes}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> FillCarMakes(int id)
        {
            await carManager.InsertCarMakes();
            return Ok();
        }

        // GET: api/Car
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Car/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Car
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Car/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
