namespace LearningDotNetCoreAPI.Services
{
    public interface IGreeter
    {
        string Greet();
    }
    public class Greeter : IGreeter
    {
        public string Greet() => "Hello from DI!";
    }
}
