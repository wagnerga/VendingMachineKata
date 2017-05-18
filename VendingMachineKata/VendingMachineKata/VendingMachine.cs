using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachineKata
{
    public class VendingMachine
    {
        public decimal CurrentAmount { get; set; }
        public List<Coin> CoinReturn { get; set; }
        private string _display { get; set; }

        public VendingMachine(decimal currentAmount, List<Coin> coinReturn, string display)
        {
            CurrentAmount = currentAmount;
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
                        CurrentAmount += GlobalConstants.QuarterValue;
                    else if (coin.IsDime())
                        CurrentAmount += GlobalConstants.DimeValue;
                    else if (coin.IsNickel())
                        CurrentAmount += GlobalConstants.NickelValue;
                }
            }

            if (CurrentAmount == 0m)
                _display = GlobalConstants.InsertCoin;
            else
                _display = CurrentAmount.ToString("C");
        }

        public Product SelectProduct(Product product)
        {
            if (CurrentAmount == product.Price)
            {
                _display = GlobalConstants.ThankYou;

                return product;
            }
            else if (CurrentAmount < product.Price)
            {
                _display = string.Format("{0} {1}", GlobalConstants.Price, product.Price.ToString("C"));

                return null;
            }

            return null;
        }

        public string CheckDisplay()
        {
            if (_display == GlobalConstants.ThankYou)
            {
                CurrentAmount = 0m;
                _display = GlobalConstants.InsertCoin;

                return GlobalConstants.ThankYou;
            }

            if (_display.Contains(GlobalConstants.Price))
            {
                var displayCopy = _display;

                if (CurrentAmount == 0m)
                    _display = GlobalConstants.InsertCoin;
                else
                    _display = CurrentAmount.ToString("C");

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
