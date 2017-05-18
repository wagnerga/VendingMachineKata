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
        public static decimal CalculateCurrentAmount(List<Coin> validCoins)
        {
            var currentAmount = 0m;

            foreach (var coin in validCoins)
            {
                if (coin.IsQuarter())
                    currentAmount += GlobalConstants.QuarterValue;
                else if (coin.IsDime())
                    currentAmount += GlobalConstants.DimeValue;
                else if (coin.IsNickel())
                    currentAmount += GlobalConstants.NickelValue;
            }

            return currentAmount;
        }
    }
}
