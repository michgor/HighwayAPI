using System.Collections.Generic;
using Highway.DAL.DomainModels.Vehicles;
using Highway.DAL.Repositories.TollStations;
using Microsoft.AspNetCore.Mvc;

namespace Highway.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TollStationController : ControllerBase
    {
        private readonly ITollStationRepository tollStationRepository;

        public TollStationController(ITollStationRepository tollStationRepository)
        {
            this.tollStationRepository = tollStationRepository;
        }

        /// <summary>
        /// Gets price that has to be paid for route.
        /// </summary>
        /// <param name="vehicleCategory">
        ///     Category of the vehicle
        ///     Samples: 1,2,3,4
        /// </param>
        /// <param name="entryGateId">
        ///     Public identifier of a entry gate in toll station
        ///     Samples: wro-w-g1, wro-w-g2, kat-g1, kat-g2
        /// </param>
        /// <param name="exitGateId">
        ///     Public identifier of a exit gate in toll station
        ///     Samples: wro-p-g1, wro-p-g2, wro-p-g3, wro-p-g4
        /// </param>
        /// <returns>Returns list of possible option for entering a gate</returns>
        /// <response code="200">Request is processed correctly and price is returned</response>
        /// <response code="404">Request was processed but parameters values don't correspond to data in database</response>
        [HttpGet]
        public ActionResult<IEnumerable<string>> GetPrice(string entryGateId, string exitGateId, VehicleCategory vehicleCategory)
        {
            var priceResult = this.tollStationRepository.GetPriceForRoute(entryGateId, exitGateId, vehicleCategory);

            if (!priceResult.IsSuccess)
            {
                return NotFound();
            }

            return Ok(priceResult.Data);
        }
    }
}
