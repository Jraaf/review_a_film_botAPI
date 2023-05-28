namespace Attempt2.Models
{
    public class Streaming
    {
        public List<Result> result { get; set; }
    }

    public class Result
    {
        public StreamingInfo streamingInfo { get; set; }
    }
    public class StreamingInfo
    {
        public Us us { get; set; }
    }
    public class Us
    {
        public Apple[]? apple { get; set; }
        public Netflix[]? netflix { get; set; }
        public Prime[]? prime { get; set; }
    }
    public class Apple
    {
        public string link { get; set; }
    }
    public class Netflix
    {
        public string link { get; set; }
    }
    public class Prime
    {
        public string link { get; set; }
    }
}
