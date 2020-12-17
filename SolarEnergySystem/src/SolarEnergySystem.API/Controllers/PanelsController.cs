using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using SolarEnergySystem.BusinessLogic.Managers;
using SolarEnergySystem.Core.Entities;
using SolarEnergySystem.BusinessLogic.Presenters;

namespace SolarEnergySystem.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PanelsController : ControllerBase
    {
        private readonly IPanelManager _panelManager;

        public PanelsController(IPanelManager panelManager)
        {
            _panelManager = panelManager;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                IEnumerable<Panel> panels = _panelManager.ListPanels();
                return Ok(PanelPresenter.DecoratePanels(panels));
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Route("report")]
        public IActionResult GetReportForPanel(string panelId)
        {
            try
            {
                return Ok(_panelManager.GetReportForPanel(panelId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
