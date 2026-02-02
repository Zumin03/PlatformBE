namespace PlatformTest.Exceptions
{
    public class DeserializationException : Exception
    {
        public string Json { get; }
        
        public DeserializationException(string json)
            : base($"An error occured during deserialization json {json}")
        {
            Json = json;
        }
    }
}
