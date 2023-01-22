using DataFileAccessLayer;
using ExternalAPI;
using Microsoft.AspNetCore.Mvc;
using Models;
using MyAPI.Models;
using System.Diagnostics;

namespace MyAPI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        ICurrencyDataService _currencyDataService;
        IDataFileService<currencyDetails> _dataFileService;
        IConfiguration _configuration;
        Dictionary<string, string> _parms;
        const string _url = "https://xecdapi.xe.com/v1/";

        public HomeController(ICurrencyDataService currencyDataService, IDataFileService<currencyDetails> dataFileService, IConfiguration configuration)
        {
            _currencyDataService = currencyDataService;
            _dataFileService = dataFileService;
            _configuration = configuration;
            _parms = _configuration.GetSection("Parms").Get<Dictionary<string, string>>();
        }

        public IActionResult Index()
        {
            _currencyDataService.GetPair(_parms);
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}