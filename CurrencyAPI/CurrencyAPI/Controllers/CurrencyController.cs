using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System;
using System.Net.Http.Headers;
using System.Buffers.Text;
using System.Text;
using System.Text.Json;
using Models;
using ExternalAPI;
using DataFileAccessLayer;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks.Dataflow;

namespace CurrencyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        ICurrencyDataService _currencyDataService;
        IDataFileService<currencyDetails> _dataFileService;
        IConfiguration _configuration;
        Dictionary<string, string> _parms;
        public CurrencyController(ICurrencyDataService currencyDataService, IDataFileService<currencyDetails> dataFileService , IConfiguration configuration)
        {
            _currencyDataService = currencyDataService;
            _dataFileService = dataFileService;
            _configuration = configuration;
            _parms =_configuration.GetSection("Parms").Get<Dictionary<string, string>>();
        }

        [HttpGet]
        public async Task SaveCurrency()
        {         
          _currencyDataService.GetPair(_parms);
        }

        

        [HttpGet("Data")]
        public List<currencyDetails> GetCurrencies()
        {         
            return _dataFileService.Read();
        }

    }
}
