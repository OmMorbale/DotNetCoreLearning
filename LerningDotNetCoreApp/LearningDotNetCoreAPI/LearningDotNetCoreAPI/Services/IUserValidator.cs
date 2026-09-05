namespace LearningDotNetCoreAPI.Services
{
    public interface IUserValidator
    {
        bool IsValid(string username, string password);
    }

    public class UserValidator : IUserValidator
    {
        public bool IsValid(string username, string password)
        {
            return username == "admin" && password == "password123";
        }

    }
}
