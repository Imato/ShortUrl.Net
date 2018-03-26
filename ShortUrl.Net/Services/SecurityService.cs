using ShortUrlNet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ShortUrlNet.Services
{
    public class SecurityService : ISecurityService
    {
        

        private IRepository _repository;
        private Configuration _configuration;

        public SecurityService(IRepository repository, IConfigurationService configuration)
        {
            _repository = repository;
            _configuration = configuration.GetConfiguration();
        }

        public User AddUser(string login, string email, string password)
        {
            var u = new User
            {
                Email = email,
                Login = login,
                Password = Encrypt(password)
            };

            // First user in app
            if(_repository.GetUsersCout() == 0)
            {
                u.IsAdmin = true;
            }

            return _repository.AddUser(u);
        }

        public bool ValidateUser(string login, string password)
        {
            var u = _repository.GetUser(login);
            if (u == null)
                return false;

            return u.Password == Encrypt(password);
        }

        public bool ValidateApiKey(string apiKey)
        {
            var key = _repository.GetApiKey(apiKey);

            if (key == null || key.ExpireDate < DateTime.UtcNow)
                return false;

            var user = _repository.GetUser(key.UserKey);

            if (user == null)
                return false;

            return true;
        }

        public ApiKey AddApiKey(User u)
        {
            var k = new ApiKey
            {
                CreateDate = DateTime.UtcNow,
                ExpireDate = DateTime.UtcNow.AddDays(_configuration.ApiKeyExpirationDays),
                UserKey = u.Key
            };

            return _repository.AddApiKey(k);
        }
        public User GetUser(string login = null, string apiKey = null)
        {
            if (login != null)
                return _repository.GetUser(login);
            if (apiKey != null)
            {
                var key = _repository.GetApiKey(apiKey);
                return _repository.GetUser(key.UserKey);
            }

            return null;
        }


        private string Encrypt(string value)
        {
            var sha256 = SHA256.Create();
            var sb = new StringBuilder();
            foreach(var b in sha256.ComputeHash(Encoding.UTF8.GetBytes(value)))
            {
                sb.Append(b.ToString("x2")); 
            }

            return sb.ToString();
        }
    }
}
