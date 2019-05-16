using System;
using System.Collections.Generic;
using Highway.DAL.Repositories.TollStations;
using Microsoft.AspNetCore.Mvc;

namespace Highway.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GateController : ControllerBase
    {
        private readonly ITollStationRepository tollStationRepository;

        public GateController(ITollStationRepository tollStationRepository)
        {
            this.tollStationRepository = tollStationRepository;
        }

        /// <summary>
        /// Gets access type for a gate in specific toll station.
        /// </summary>
        /// <param name="tollStationId">
        ///     Public identifier of toll station
        ///     Samples: wro-w, wro-p, kat
        /// </param>
        /// <param name="entryGateId">
        ///     Public identifier of a gate in toll station
        ///     Samples: wro-w-g1, wro-w-g2, wro-p-g1, wro-p-g2, wro-p-g3, wro-p-g4, kat-g1, kat-g2
        /// </param>
        /// <returns>Returns list of possible option for entering a gate</returns>
        /// <response code="200">Request is processed correctly and list of possible options was returned</response>
        /// <response code="404">Request was processed but parameters values don't correspond to data in database</response>
        [HttpGet]
        public ActionResult<IEnumerable<string>> GetAccessTypes(string tollStationId, string entryGateId)
        {
            var gateAccessTypeResult = this.tollStationRepository.GetGateAccessTypes(tollStationId, entryGateId);

            if (!gateAccessTypeResult.IsSuccess)
            {
                return NotFound();
            }

            return Ok(gateAccessTypeResult.Data);
        }        
    }
}
