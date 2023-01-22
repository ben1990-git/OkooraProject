using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Converters
{
    public interface IConvertToStringService
    {
        List<Task<string>> ConvertAllAsync(List<Task<currencyDetails>> tasks);
    }
}
