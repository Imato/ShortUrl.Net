using System;
using ShortUrlNet.Models;

namespace ShortUrlNet.Services
{
    public class UrlService
    {
        private IRepository _repository;
        public UrlService(IRepository repository)
        {
            _repository = repository;
        }

        public ShortUrl AddUrl(string url, string apiKey)
        {
            var key = _repository.GetApiKey(apiKey);

            if (key == null || key.ExpireDate < DateTime.UtcNow)
                throw new UnauthorizedAccessException();

            var user = _repository.GetUser(key.UserKey);
            
            var u = new ShortUrl
            {
                CreateDate = DateTime.UtcNow,
                Url = url,
                UserKey = user.Key
            };

            return _repository.AddUrl(u);
        }

        public ShortUrl GetUrl(string key)
        {
            var u = _repository.GetUrl(key);
            _repository.ViewUrl(u);
            return u;
        }
    }


}
