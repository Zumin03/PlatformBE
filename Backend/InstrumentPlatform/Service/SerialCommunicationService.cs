namespace InstrumentPlatform.Service
{
    using System.IO.Ports;
    using InstrumentPlatform.Enums;
    using InstrumentPlatform.Extensions;
    using InstrumentPlatform.Handlers;

    public class SerialCommunicationService : ISerialCommunicationService
    {
        private readonly ILogger<ISerialCommunicationService> logger;
        private readonly IInstrumentErrorHandler instrumentErrorHandler;

        /// <inheritdoc/>
        public SerialCommunicationService(
            ILogger<ISerialCommunicationService> logger,
            IInstrumentErrorHandler instrumentErrorHandler)
        {
            this.logger = logger;
            this.instrumentErrorHandler = instrumentErrorHandler;
        }

        /// <inheritdoc/>
        public string SendCommand(InstrumentCommand command, string port, int baudRate, int timeOut = 5000)
        {
            logger.LogInformation($"Opening serial port \"{port}\" with baud rate: \"{baudRate}\"");
            using var serial = new SerialPort(port, baudRate);
            serial.Encoding = System.Text.Encoding.UTF8;
            serial.ReadTimeout = timeOut;
            serial.Open();

            logger.LogInformation($"Executing command: \"{command}\" on serial port: \"{port}\"");
            serial.WriteLine(command.ToCommandString());
            var response = serial.ReadLine().Trim();
            serial.Close();

            return response;
        }
    }
}
