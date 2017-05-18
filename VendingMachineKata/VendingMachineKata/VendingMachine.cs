using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachineKata
{
    public class VendingMachine
    {
        private decimal _insertedAmount { get; set; }
        public List<Coin> CoinReturn { get; set; }

        public VendingMachine(List<Coin> coinReturn)
        {
            CoinReturn = coinReturn;
        }

        public void AcceptCoins(List<Coin> coins)
        {
            foreach (var coin in coins)
            {
                if (!IsValidCoin(coin))
                    CoinReturn.Add(coin);
                else
                {
                    if (coin.IsQuarter())
                        _insertedAmount += GlobalConstants.QuarterValue;
                    else if (coin.IsDime())
                        _insertedAmount += GlobalConstants.DimeValue;
                    else if (coin.IsNickel())
                        _insertedAmount += GlobalConstants.NickelValue;
                }
            }
        }

        public bool IsValidCoin(Coin coin)
        {
            return (coin.IsQuarter() || coin.IsDime() || coin.IsNickel());
        }

        public string GetDisplay()
        {
            if (_insertedAmount == 0)
                return GlobalConstants.NoCoinsDisplay;
            else
                return _insertedAmount.ToString("C");
        }
    }
}
