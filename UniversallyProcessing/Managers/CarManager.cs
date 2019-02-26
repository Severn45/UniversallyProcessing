using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UniversallyProcessing.Core.Models;
using UniversallyProcessing.Core.Repository;

namespace UniversallyProcessing.Api.Managers
{
    public class CarManager
    {
        private readonly IRepository<Make, int> makeRepository;
        public CarManager(IRepository<Make, int> makeRepository)
        {
            this.makeRepository = makeRepository;
        }

        public async Task<IEnumerable<Make>> GetCarMakes()
        {
            var carCollection = makeRepository.Get();
            return await carCollection;
        }
        
        public async Task<IActionResult> InsertCarMakes()
        {
            var carMakeList = new List<Make>();

            try
            {
                foreach (var make in carMakeList)
                {
                    await makeRepository.Create(make);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return new OkResult();
        }
    }
}
