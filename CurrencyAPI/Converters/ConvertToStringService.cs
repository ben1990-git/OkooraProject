using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Converters
{
    public class ConvertToStringService : IConvertToStringService
    {
        StringBuilder _stringBuilder;
        public ConvertToStringService(StringBuilder stringBuilder)
        {
            _stringBuilder = stringBuilder;
        }
     

        //public List<Task<string>> ConvertAll(List<currencyPair> currencyPairs)
        //{
        //    foreach (var currencyPair in currencyPairs)
        //    {               
        //        foreach (var item in currencyPair.from)
        //        {
        //            _stringBuilder.AppendLine(item.quotecurrency+"/"+ currencyPair.to+","+"value:"+ item.mid+"/"+ item.inverse +"date:"+ currencyPair.timestamp.ToString());
        //        }
        //    }
        //}

        public List<Task<string>> ConvertAllAsync(List<Task<currencyDetails>> tasks)
        {
            throw new NotImplementedException();
        }

        //public async List<Task<string>> ConvertAllAsync(List<Task<currencyPair>> currencyPairsTasks)
        //{
        //    foreach (var currencyPairTask in currencyPairsTasks)
        //    {
        //        foreach (var item in currencyPairTask)
        //        {
        //            _stringBuilder.AppendLine(item.quotecurrency + "/" + currencyPair.to + "," + "value:" + item.mid + "/" + item.inverse + "date:" + currencyPair.timestamp.ToString());
        //        }
        //    }
        //}
    }
}
