using DataFileAccessLayer;
using Microsoft.Extensions.Configuration;
using Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using System.Xml.Linq;


namespace ExternalAPI
{
    public class XeCurrencyDataAPIAccess : APIAccess, ICurrencyDataService
    {
        const string _scheme = "Basic";
        IConfiguration _configuration;
        IDataFileService<currencyDetails> _dataFileService;
        const string _url = "https://xecdapi.xe.com/v1/";
        HttpClient _client;
        ActionBlock<currencyDetails?> ?_createCurrencyWriter;          
        public XeCurrencyDataAPIAccess(IConfiguration configuration, IDataFileService<currencyDetails> dataFileService)
        {

            _configuration = configuration;
            _dataFileService = dataFileService;
            _client = new HttpClient();
            _createCurrencyWriter = new ActionBlock<currencyDetails?>(x => _dataFileService.Write(x));
        }
        public  void GetPair(Dictionary<string, string> parms)
        {        
            SaveCurrncies(parms);
        }
     
        private void SaveCurrncies(Dictionary<string, string> parms)
        {
            var currencyReader = new TransformBlock<string, string>(SendRequest);
            var convretStringCurrencyDetails = new TransformBlock<string, currencyDetails?>(x => JsonSerializer.Deserialize<currencyDetails>(x));
            
             _createCurrencyWriter = new ActionBlock<currencyDetails?>(x => _dataFileService.Write(x));
            var linkOptions = new DataflowLinkOptions { PropagateCompletion = true };
            var blockOption = new DataflowBlockOptions { BoundedCapacity = 100};
            var bufferBlock = new BufferBlock<string>( blockOption);

            bufferBlock.LinkTo(currencyReader,linkOptions);
            currencyReader.LinkTo(convretStringCurrencyDetails, linkOptions);
            convretStringCurrencyDetails.LinkTo(_createCurrencyWriter);

            Task.Run(async() =>
            {
                while (true)
                {
                    var list = 
                    parms.Select(parm => {
                       return bufferBlock.SendAsync($"{_url}convert_from?from={parm.Key}&to={parm.Value}&inverse=true&decimal_places=3");
                        
                    });

                  await Task.WhenAll(list.ToArray());
                  await  Task.Delay(TimeSpan.FromSeconds(10));                                
                }               
            });   
        }

        private async Task<string> SendRequest(string url)
        {
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
            AuthenticatRequest(requestMessage);
            HttpResponseMessage responseMessage = await _client.SendAsync(requestMessage);            
            return await responseMessage.Content.ReadAsStringAsync();
        }
       protected override void AuthenticatRequest(HttpRequestMessage requestMessage)
        {
            string encodedStr = _configuration.GetSection("encodedStr").Value.ToString();
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue(_scheme, encodedStr);
        }
    }
}


