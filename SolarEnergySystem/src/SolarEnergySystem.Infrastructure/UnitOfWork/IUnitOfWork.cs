using SolarEnergySystem.Core.Entities;
using SolarEnergySystem.Infrastructure.Repositories;
using System;
using System.Threading.Tasks;

namespace SolarEnergySystem.Infrastructure.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        EntityFrameworkRepository<Panel,string> PanelRepository { get; }
        EntityFrameworkRepository<ElectricityReading,int> ElectricityReadingRepository { get; }

        Task Commit();
    }
}
