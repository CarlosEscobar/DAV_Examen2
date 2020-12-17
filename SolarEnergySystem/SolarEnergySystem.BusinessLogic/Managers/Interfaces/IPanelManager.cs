using SolarEnergySystem.Core.Entities;
using System.Collections.Generic;

namespace SolarEnergySystem.BusinessLogic.Managers
{
    public interface IPanelManager
    {
        IEnumerable<Panel> ListPanels();
        PanelReportDTO GetReportForPanel(string panelId);
    }
}
