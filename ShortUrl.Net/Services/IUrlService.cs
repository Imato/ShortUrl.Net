using ShortUrlNet.Models;

namespace ShortUrlNet.Services
{
    public interface IUrlService
    {
        ShortUrl AddUrl(string url, User user);
        ShortUrl GetUrl(string key);
    }
}