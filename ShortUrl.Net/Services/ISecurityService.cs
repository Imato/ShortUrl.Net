using ShortUrlNet.Models;

namespace ShortUrlNet.Services
{
    public interface ISecurityService
    {
        ApiKey AddApiKey(User u);
        User AddUser(string login, string email, string password);
        User GetUser(string login = null, string apiKey = null);
        bool ValidateUser(string login, string password);
        bool ValidateApiKey(string apiKey);
        ApiKey GetApiKey(User user);
    }
}