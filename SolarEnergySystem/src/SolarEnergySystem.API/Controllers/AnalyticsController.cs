using System;
using Microsoft.AspNetCore.Mvc;
using SolarEnergySystem.BusinessLogic;
using SolarEnergySystem.BusinessLogic.Managers;
using SolarEnergySystem.Core;
using SolarEnergySystem.Core.Entities;

namespace SolarEnergySystem.API.Controllers
{
    [ApiController]
    [Route("panels")]
    public class AnalyticsController : ControllerBase
    {
        private readonly IElectricityReadingManager _electricityReadingManager;

        public AnalyticsController(IElectricityReadingManager electricityReadingManager)
        {
            _electricityReadingManager = electricityReadingManager;
        }

        [HttpPost]
        public IActionResult RegisterReading([FromBody] RegisterReading reading)
        {
            try
            {
                ElectricityReading electricityReading = _electricityReadingManager.RegisterReading(reading);
                return Ok(ServiceResult<ElectricityReading>.SuccessResult(electricityReading));
            }
            catch (Exception ex)
            {
                return BadRequest(ServiceResult<ElectricityReading>.ErrorResult(ex.Message));
            }
        }
    }
}
