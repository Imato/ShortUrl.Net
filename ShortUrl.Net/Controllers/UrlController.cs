using Microsoft.AspNetCore.Mvc;
using ShortUrlNet.Models;
using ShortUrlNet.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortUrlNet.Controllers
{
    public class UrlController : Controller
    {
        private ISecurityService _security;
        private IUrlService _urlService;

        public UrlController(ISecurityService security, IUrlService urlService)
        {
            _security = security;
            _urlService = urlService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [NonAction]
        private User GetUser()
        {
            var apiKey = string.Empty;
            Request.Cookies.TryGetValue(Configuration.API_KEY_COOKIE, out apiKey);

            if (!_security.ValidateApiKey(apiKey))
                return null;

            var user = _security.GetUser(null, apiKey);
            return user;
        }

        [HttpPost]
        public IActionResult Create([FromBody] string url)
        {
            var user = GetUser();
            if (user == null)
                RedirectToAction("Login");

            var newUrl = _urlService.AddUrl(url, user);

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("{urlKey}")]
        public IActionResult Get(string urlKey)
        {
            var url = _urlService.GetUrl(urlKey);

            if (url == null || string.IsNullOrEmpty(url.Url))
                return NotFound();

            
            return RedirectToPagePermanent(url.Url);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Authenticate([FromBody] string login, [FromBody] string password)
        {
            if (_security.ValidateUser(login, password))
            {
                var user = _security.GetUser(login);
                var apiKey = _security.GetApiKey(user);
                Response.Cookies.Append(Configuration.API_KEY_COOKIE, apiKey.Key.ToString());
                return RedirectToAction("Index");
            }
            else
                return RedirectToAction("Login");
        }

    }
}
