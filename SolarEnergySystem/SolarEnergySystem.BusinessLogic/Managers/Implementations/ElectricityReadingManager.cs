using SolarEnergySystem.Core.Entities;
using SolarEnergySystem.Core.Enums;
using SolarEnergySystem.Infrastructure.UnitOfWork;
using System;
using System.Linq;

namespace SolarEnergySystem.BusinessLogic.Managers
{
    public class ElectricityReadingManager : IElectricityReadingManager
    {
        private readonly IUnitOfWork unitOfWork;

        public ElectricityReadingManager(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public ElectricityReading RegisterReading(RegisterReading reading)
        {
            try
            {
                if(reading.Value <= 0)
                {
                    throw new Exception("Valor incorrecto.");
                }

                Panel thePanel = unitOfWork.PanelRepository.GetById(reading.PanelId);
                double kiloWattReading = reading.Value;
                if (thePanel.MeasuringUnit == MeasuringUnit.Watt)
                {
                    kiloWattReading /= 1000;
                }

                //Reading Restrictions
                ElectricityReading lastReading = thePanel.ElectricityReadings.OrderByDescending(reading => reading.ReadingDateTime).FirstOrDefault();
                TimeSpan diffWithPresent = (DateTime.UtcNow - lastReading.ReadingDateTime);
                switch (thePanel.PanelType)
                {
                    case PanelType.Regular:
                        if (diffWithPresent.TotalHours < 1)
                        {
                            throw new Exception("No ha pasado 1 hora desde el ultimo registro.");
                        }
                        break;
                    case PanelType.Limited:
                        if (diffWithPresent.TotalDays < 1)
                        {
                            throw new Exception("No ha pasado 1 hora desde el ultimo registro.");
                        }
                        if(kiloWattReading < 5)
                        {
                            throw new Exception("No se puede registrar menos de 5 kilowatts.");
                        }
                        break;
                    case PanelType.Ultimate:
                        if (diffWithPresent.TotalMinutes < 1)
                        {
                            throw new Exception("No ha pasado 1 hora desde el ultimo registro.");
                        }
                        if (kiloWattReading > 5)
                        {
                            throw new Exception("No se puede registrar mas de 5 kilowatts.");
                        }
                        break;
                    default:
                        break;
                }

                ElectricityReading newElectricityReading = new ElectricityReading()
                {
                    PanelId = reading.PanelId,
                    Panel = thePanel,
                    KiloWatt = kiloWattReading,
                    ReadingDateTime = DateTime.UtcNow
                };

                unitOfWork.ElectricityReadingRepository.Add(newElectricityReading);
                unitOfWork.Commit();

                return newElectricityReading;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
