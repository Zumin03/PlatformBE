using System.ComponentModel;

namespace PlatformTest.Enums
{
    public enum InstrumentState
    {
        [Description("Connected")]
        Connected = 0,

        [Description("Disconnected")]
        Disconnected = 1,

        [Description("Faulted")]
        Faulted = 2,
    }
}
