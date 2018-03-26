using Microsoft.AspNetCore.Mvc;
using ShortUrlNet.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShortUrlNet.Controllers
{
    public class AdminController : Controller
    {
        private ISecurityService _security;

        public AdminController(ISecurityService security)
        {
            _security = security;
        }

        public IActionResult Index()
        {

        }
    }
}
