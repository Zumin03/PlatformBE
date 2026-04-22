namespace InstrumentPlatform.Exceptions
{
    /// <summary>
    /// The exception that is thrown when JSON deserialization fails.
    /// </summary>
    public class DeserializationException : Exception
    {
        public string Json { get; }
        
        public DeserializationException(string json)
            : base($"An error occurred during deserialization json {json}")
        {
            Json = json;
        }
    }
}
