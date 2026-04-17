using InstrumentPlatform.Options;
using Microsoft.Extensions.Options;
using TimeZoneConverter;

namespace InstrumentPlatform.Service
{
    public class TimeService : ITimeService
    {
        private readonly TimeZoneInfo timeZoneInfo;

        public TimeService(IOptions<TimeZoneOptions> options)
        {
            timeZoneInfo = TZConvert.GetTimeZoneInfo(options.Value.Default);
        }

        /// <inheritdoc/>
        public DateTime AdjustTimeToTimezone(DateTime time)
        {
            return TimeZoneInfo.ConvertTimeFromUtc(time, timeZoneInfo);
        }

        /// <inheritdoc/>
        public DateTime GenerateUtcTimestamp()
        {
            var dateTime = DateTime.UtcNow;
            dateTime = new DateTime(
                dateTime.Ticks - (dateTime.Ticks % TimeSpan.TicksPerSecond),
                dateTime.Kind);

            return dateTime;
        }
    }
}
