namespace InstrumentPlatform.Service
{
    public interface ITimeService
    {
        /// <summary>
        /// Converts a UTC DateTime value to the configured target time zone.
        /// </summary>
        /// <param name="time">The UTC DateTime to convert.</param>
        /// <returns>The <see cref="DateTime"/> converted to the configured time zone.</returns>
        DateTime AdjustTimeToTimezone(DateTime time);

        /// <summary>
        /// Generates a UTC timestamp with precision truncated to the nearest second.
        /// This removes sub-second precision (milliseconds and ticks below one second).
        /// </summary>
        /// <returns>A UTC <see cref="DateTime"/> representing the current time rounded down to whole seconds.</returns>
        DateTime GenerateUtcTimestamp();
    }
}
