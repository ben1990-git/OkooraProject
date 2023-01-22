using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class currencyDetails
    {       
        public string from { get; set; }
        public double amount { get; set; }
        public DateTime timestamp { get; set; }
        public List<CurrencyToConvert> to { get; set; }
    }
}
