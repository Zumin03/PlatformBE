using InstrumentPlatform.Options;
using InstrumentPlatform.Service;
using Microsoft.Extensions.Options;

namespace IntrumentPlatform.Test
{
    public class TimeServiceTest
    {
        [Fact]
        public void AdjustTimeToTimezone_WinterTime()
        {
            // Arrange
            var input = new DateTime(2025, 1, 1, 8, 0, 20, DateTimeKind.Utc);
            var options = Options.Create(new TimeZoneOptions
            {
                Default = "Europe/Budapest"
            });

            var service = new TimeService(options);

            // Act
            var result = service.AdjustTimeToTimezone(input);

            // Assert
            Assert.Equal(9, result.Hour);
        }

        [Fact]
        public void AdjustTimeToTimezone_SummerTime()
        {
            // Arrange
            var input = new DateTime(2025, 6, 12, 10, 0, 20, DateTimeKind.Utc);
            var options = Options.Create(new TimeZoneOptions
            {
                Default = "Europe/Budapest"
            });

            var service = new TimeService(options);

            // Act
            var result = service.AdjustTimeToTimezone(input);

            // Assert
            Assert.Equal(12, result.Hour);
        }

        [Fact]
        public void GenerateUtcTimestamp_ShouldReturnUtcTime_TruncatedToSeconds()
        {
            // Arrange
            var options = Options.Create(new TimeZoneOptions
            {
                Default = "Europe/Budapest"
            });

            var service = new TimeService(options);

            // Act
            var result = service.GenerateUtcTimestamp();

            //Assert
            Assert.Equal(DateTimeKind.Utc, result.Kind);
            Assert.Equal(0, result.Millisecond);
            Assert.Equal(0, result.Ticks % TimeSpan.TicksPerSecond);
        }
    }
}
