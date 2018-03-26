using ShortUrlNet.Models;

namespace ShortUrlNet.Services
{
    public class TestDataService
    {
        private ISecurityService _security;
        private IUrlService _urlService;

        public TestDataService(ISecurityService security, IUrlService urlService)
        {
            _security = security;
            _urlService = urlService;
        }

        public void LoadData()
        {
            var user = _security.AddUser("TestUser", "TestUser@mail.ru", "SamePassword");
            var apiKey = _security.AddApiKey(user);
            var url1 = _urlService.AddUrl("https://www.typescriptlang.org/docs/handbook/modules.html", user);
            var url2 = _urlService.AddUrl("https://habrahabr.ru/company/newprolab/blog/351616/", user);

        }
    }
}
