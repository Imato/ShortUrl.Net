using System;
using ShortUrlNet.Models;

namespace ShortUrlNet.Services
{
    public class UrlService : IUrlService
    {
        private IRepository _repository;
        public UrlService(IRepository repository)
        {
            _repository = repository;
        }

        public ShortUrl AddUrl(string url, User user)
        {           
            
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

            if (u == null)
                throw new ApplicationException($"URL {key} not found");

            _repository.ViewUrl(u);
            return u;
        }
    }


}
