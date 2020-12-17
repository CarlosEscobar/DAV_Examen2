using SolarEnergySystem.Core.Entities;
using SolarEnergySystem.Core.Enums;
using System.Collections.Generic;
using System.Linq;

namespace SolarEnergySystem.BusinessLogic.Presenters
{
    public static class PanelPresenter
    {
        public static IEnumerable<ListPanelDTO> DecoratePanels(IEnumerable<Panel> panels)
        {
            return panels.Select(panel => new ListPanelDTO()
            {
                Brand = panel.Brand,
                MeasuringUnit = CastMeasuringUnit(panel.MeasuringUnit),
                PanelType = CasePanelType(panel.PanelType)
            });
        }

        private static string CastMeasuringUnit(MeasuringUnit measuringUnit)
        {
             switch(measuringUnit)
            {
                case MeasuringUnit.Watt:
                    return "Watt";
                case MeasuringUnit.KiloWatt:
                    return "KiloWatt";
                default:
                    return "";
            }
        }

        private static string CasePanelType(PanelType panelType)
        {
            switch(panelType)
            {
                case PanelType.Limited:
                    return "Limited";
                case PanelType.Regular:
                    return "Regular";
                case PanelType.Ultimate:
                    return "Ultimate";
                default:
                    return "";
            }
        }
    }
}
