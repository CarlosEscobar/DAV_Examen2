using System.Collections.Generic;

namespace SolarEnergySystem.BusinessLogic
{
    public class KiloWattHourData
    {
        public double Sum { get; set; }
        public double Average { get; set; }
        public double Maximum { get; set; }
        public double Minimum { get; set; }
    }
    public class PanelReportDTO
    {
        public List<KiloWattHourData> ReportData { get; set; }
    }
}