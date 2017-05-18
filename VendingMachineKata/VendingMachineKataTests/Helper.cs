using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using VendingMachineKata;

namespace VendingMachineKataTests
{
    public static class Helper
    {
        public static decimal CalculateAmount(List<Coin> validCoins)
        {
            var amount = 0m;

            foreach (var coin in validCoins)
            {
                if (coin.IsQuarter())
                    amount += GlobalConstants.QuarterValue;
                else if (coin.IsDime())
                    amount += GlobalConstants.DimeValue;
                else if (coin.IsNickel())
                    amount += GlobalConstants.NickelValue;
            }

            return amount;
        }
    }
}
