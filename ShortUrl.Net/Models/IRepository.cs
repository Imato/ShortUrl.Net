using LiteDB;
using System.Collections.Generic;

namespace ShortUrlNet.Models
{
    public interface IRepository
    {
        ApiKey AddApiKey(ApiKey key);
        ShortUrl AddUrl(ShortUrl url);
        User AddUser(User user);
        ApiKey GetApiKey(string key);
        ShortUrl GetUrl(string key);
        User GetUser(string login);
        User GetUser(ObjectId key);
        void ViewUrl(ShortUrl url);
        IEnumerable<User> GetUsers();
        int GetUsersCout();
    }
}