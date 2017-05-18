using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachineKata
{
    public class VendingMachine
    {
        public decimal AmountInserted { get; set; }
        public List<Coin> CoinReturn { get; set; }
        private string _display { get; set; }

        public VendingMachine(decimal amountInserted, List<Coin> coinReturn, string display)
        {
            AmountInserted = amountInserted;
            CoinReturn = coinReturn;
            _display = display;
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
                        AmountInserted += GlobalConstants.QuarterValue;
                    else if (coin.IsDime())
                        AmountInserted += GlobalConstants.DimeValue;
                    else if (coin.IsNickel())
                        AmountInserted += GlobalConstants.NickelValue;
                }
            }

            if (AmountInserted == 0m)
                _display = GlobalConstants.InsertCoin;
            else
                _display = AmountInserted.ToString("C");
        }

        public Product SelectProduct(Product product)
        {
            if (AmountInserted == product.Price)
            {
                _display = GlobalConstants.ThankYou;

                return product;
            }
            if (AmountInserted > product.Price)
            {
                CoinReturn.AddRange(MakeChange(AmountInserted - product.Price));
            }
            else if (AmountInserted < product.Price)
            {
                _display = string.Format("{0} {1}", GlobalConstants.Price, product.Price.ToString("C"));

                return null;
            }

            return null;
        }

        public List<Coin> MakeChange(decimal amount)
        {
            var coins = new List<Coin>();

            var quarters = (int)Math.Floor(amount / GlobalConstants.QuarterValue);
            amount %= GlobalConstants.QuarterValue;
            var dimes = (int)Math.Floor(amount / GlobalConstants.DimeValue);
            amount %= GlobalConstants.DimeValue;
            var nickels = (int)Math.Floor(amount / GlobalConstants.NickelValue);

            for (var i = 0; i < quarters; i++)
            {
                coins.Add(new Coin(GlobalConstants.QuarterGrams, GlobalConstants.QuarterDiameter));
            }

            for (var i = 0; i < dimes; i++)
            {
                coins.Add(new Coin(GlobalConstants.DimeGrams, GlobalConstants.DimeDiameter));
            }

            for (var i = 0; i < nickels; i++)
            {
                coins.Add(new Coin(GlobalConstants.NickelGrams, GlobalConstants.NickelDiameter));
            }

            return coins;
        }

        public string CheckDisplay()
        {
            if (_display == GlobalConstants.ThankYou)
            {
                AmountInserted = 0m;
                _display = GlobalConstants.InsertCoin;

                return GlobalConstants.ThankYou;
            }

            if (_display.Contains(GlobalConstants.Price))
            {
                var displayCopy = _display;

                if (AmountInserted == 0m)
                    _display = GlobalConstants.InsertCoin;
                else
                    _display = AmountInserted.ToString("C");

                return displayCopy;
            }

            return _display;
        }

        public bool IsValidCoin(Coin coin)
        {
            return (coin.IsQuarter() || coin.IsDime() || coin.IsNickel());
        }
    }
}
