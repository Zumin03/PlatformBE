using PlatformTest.Entities;
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
    }
}
