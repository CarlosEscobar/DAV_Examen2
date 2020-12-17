using SolarEnergySystem.BusinessLogic;
using SolarEnergySystem.BusinessLogic.Presenters;
using SolarEnergySystem.Core.Entities;
using SolarEnergySystem.Core.Enums;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SolarEnergySystem.Tests
{
    public class PanelPresenterTest
    {
        [Theory]
        [InlineData("KiloWatt")]
        public void PresenterShouldMapPanelUnit(string expectedUnit)
        {
            // Arrange
            IEnumerable<Panel> panels = new List<Panel>()
            {
                new Panel()
                {
                    Id = "Panel1",
                    PanelType = PanelType.Regular,
                    Brand = "Brand1",
                    MeasuringUnit = MeasuringUnit.KiloWatt,
                    Latitude = 0.005,
                    Longitude = 0.005,
                }
            };

            // Act
            ListPanelDTO mappedPanel = PanelPresenter.DecoratePanels(panels).FirstOrDefault();

            // Assert
            Assert.Equal(mappedPanel.MeasuringUnit, expectedUnit);
        }

        [Theory]
        [InlineData("Ultimate")]
        public void PresenterShouldMapPanelType(string expectedPanelType)
        {
            // Arrange
            IEnumerable<Panel> panels = new List<Panel>()
            {
                new Panel()
                {
                    Id = "Panel2",
                    PanelType = PanelType.Ultimate,
                    Brand = "Brand2",
                    MeasuringUnit = MeasuringUnit.Watt,
                    Latitude = 0.009,
                    Longitude = 0.009,
                }
            };

            // Act
            ListPanelDTO mappedPanel = PanelPresenter.DecoratePanels(panels).FirstOrDefault();

            // Assert
            Assert.Equal(mappedPanel.PanelType, expectedPanelType);
        }
    }
}
