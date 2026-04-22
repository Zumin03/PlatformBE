using InstrumentPlatform.Enums;

namespace InstrumentPlatform.Service
{
    public interface ISerialCommunicationService
    {
        /// <summary>
        /// Executes a command on a device via serial communication and returns its response.
        /// </summary>
        /// <param name="command">The InstrumentCommand enum contains the numerical identifier of a command.</param>
        /// <param name="port">The serial port identifier</param>
        /// <param name="baudRate">The communication speed in bits per second.</param>
        /// <param name="timeOut">The timeout in milliseconds for the request.</param>
        /// <returns>The device response read from the serial port.</returns>
        string SendCommand(InstrumentCommand command, string port, int baudRate, int timeOut = 5000);
    }
}
