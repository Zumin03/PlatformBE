using System.Diagnostics.Metrics;
using System.Reflection.Metadata;

namespace InstrumentPlatform.Handlers
{
    public interface IInstrumentErrorHandler
    {
        /// <summary>
        /// Handles a communication error for the specified instrument by
        /// setting its state to faulted and persisting the change.
        /// </summary>
        /// <param name="deviceId">The identifier of the instrument that encountered the error.</param>
        /// <returns>The identifier of the affected instrument.</returns>
        Task<string> HandleCommunicationError(string deviceId);
    }
}
