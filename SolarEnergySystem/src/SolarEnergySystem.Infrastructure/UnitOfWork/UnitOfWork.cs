using SolarEnergySystem.Core.Entities;
using SolarEnergySystem.Infrastructure.Repositories;
using System.Threading.Tasks;

namespace SolarEnergySystem.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SolarEnergySystemDatabaseContext dbContext;
        public EntityFrameworkRepository<Panel, string> PanelRepository { get; private set; }
        public EntityFrameworkRepository<ElectricityReading, int> ElectricityReadingRepository { get; private set; }

        public UnitOfWork(SolarEnergySystemDatabaseContext context)
        {
            dbContext = context;
            PanelRepository = new EntityFrameworkRepository<Panel, string>(dbContext);
            ElectricityReadingRepository = new EntityFrameworkRepository<ElectricityReading, int>(dbContext);
        }

        public void Dispose()
        {
            dbContext.Dispose();
        }

        public async Task Commit()
        {
            await dbContext.SaveChangesAsync();
        }
    }
}
