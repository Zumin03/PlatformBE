using InstrumentPlatform.Enums;

namespace InstrumentPlatform.Extensions
{
    /// <summary>
    /// Provides extension methods for InstrumentCommand enum.
    /// </summary>
    public static class InstrumentCommandExtension
    {
        /// <summary>
        /// Converts an <see cref="InstrumentCommand"/> value to its corresponding command string representation used in serial communication.
        /// </summary>
        /// <param name="command">The instrument command to convert.</param>
        /// <returns>The string representation of the command that can be sent to the device.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the provided command is not a valid <see cref="InstrumentCommand"/> value.</exception>
        public static string ToCommandString(this InstrumentCommand command)
        {
            return command switch
            {
                InstrumentCommand.Identify => "IDENTIFY",
                InstrumentCommand.SelfTest => "SELFTEST",
                InstrumentCommand.Measure => "MEASURE",
                _ => throw new ArgumentOutOfRangeException(nameof(command), command, null)
            };
        }
    }
}
