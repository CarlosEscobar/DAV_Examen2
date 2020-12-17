using SolarEnergySystem.Core.Entities;
using SolarEnergySystem.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SolarEnergySystem.BusinessLogic.Managers
{
    public class PanelManager : IPanelManager
    {
        private readonly IUnitOfWork unitOfWork;

        public PanelManager(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<Panel> ListPanels()
        {
            return unitOfWork.PanelRepository.GetAll().OrderBy(panel => panel.PanelType);
        }

        public PanelReportDTO GetReportForPanel(string panelId)
        {
            return new PanelReportDTO()
            {
                ReportData = unitOfWork.PanelRepository
                                       .GetById(panelId)
                                       .ElectricityReadings.Where(reading => reading.ReadingDateTime.Date.Date == DateTime.Now.Date
                                                                          && reading.ReadingDateTime.Date.Month == DateTime.Now.Month
                                                                          && reading.ReadingDateTime.Date.Year == DateTime.Now.Year)
                                                           .GroupBy(reading => reading.ReadingDateTime.Hour)
                                                           .Select(readingGroup => new KiloWattHourData()
                                                           {
                                                               Sum = readingGroup.Sum(reading => reading.KiloWatt),
                                                               Average = readingGroup.Average(reading => reading.KiloWatt),
                                                               Maximum = readingGroup.Max(reading => reading.KiloWatt),
                                                               Minimum = readingGroup.Min(reading => reading.KiloWatt)
                                                           })
                                                           .ToList()
            };
        }
    }
}
