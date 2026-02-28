using PlatformTest.Entities;
using PlatformTest.Enums;
using PlatformTest.Model;

namespace PlatformTest.Service
{
    public interface IInstrumentService
    {
        /// <summary>
        /// Deserializes instrument identification response JSON files to DTO objects.
        /// </summary>
        /// <param name="instrumentJSON">The instrument response to deserialize.</param>
        /// <returns>Return a <see cref="InstrumentEntity"/></returns>
        InstrumentEntity DeserializeInstrument(string instrumentJSON);
        
        /// <summary>
        /// Deserializes instrument self test response JSON files to DTO objects
        /// </summary>
        /// <param name="selfTestJSON"></param>
        /// <returns>Returns a <see cref="SelfTestDTO"/></returns>
        SelfTestDTO DeserializeSelfTest(string selfTestJSON);

        /// <summary>
        /// Detects instruments by checking communication ports.
        /// </summary>
        Task DetectInstrumentsAsync();

        /// <summary>
        /// Retrieves all recorded instruments and returns them in DTO format.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<InstrumentDTO>> GetInstrumentsAsync();
        
        /// <summary>
        /// After the deserialization of the self test json returns the state of the instrument.
        /// </summary>
        /// <param name="selfTestJSON">The self test json received from the instrument.</param>
        /// <returns>Returns an <see cref="InstrumentState"/> representing the state of the instrument.</returns>
        InstrumentState GetSelfTestResult(string selfTestJSON);

        /// <summary>
        /// Runs a self test on a specific instrument.
        /// </summary>
        /// <param name="id">The id of the instrument to run the self test on.</param>
        /// <returns>Returns a <see cref="InstrumentDTO"/> with the updated state.</returns>
        Task<InstrumentDTO> RunSelfTest(string id);
    }
}
