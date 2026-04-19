using InstrumentPlatform.Enums;
using InstrumentPlatform.Service;

namespace InstrumentPlatform.Handlers
{
    /// <summary>
    /// Provides functionality for handling instrument-related errors.
    /// </summary>
    public class InstrumentErrorHandler : IInstrumentErrorHandler
    {
        private readonly IRepositoryService repositoryService;

        public InstrumentErrorHandler(IRepositoryService repositoryService)
        {
            this.repositoryService = repositoryService;
        }

        /// <inheritdoc/>
        public async Task<string> HandleCommunicationError(string deviceId)
        {
            var instrument = await repositoryService.GetInstrumentById(deviceId);
            instrument.State = InstrumentState.Faulted;
            await repositoryService.RegisterInstrument(instrument);

            return instrument.Id;
        }
    }
}
