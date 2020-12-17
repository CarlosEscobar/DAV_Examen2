using SolarEnergySystem.Core.Entities;

namespace SolarEnergySystem.BusinessLogic.Managers
{
    public interface IElectricityReadingManager
    {
        public ElectricityReading RegisterReading(RegisterReading reading);
    }
}
