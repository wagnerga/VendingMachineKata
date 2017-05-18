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
        public List<Product> Products { get; set; }
        private decimal _totalAmount { get; set; }

        public VendingMachine(decimal amountInserted, List<Coin> coinReturn, string display, List<Product> products, decimal totalAmount)
        {
            AmountInserted = amountInserted;
            CoinReturn = coinReturn;
            _display = display;
            Products = products;
            _totalAmount = totalAmount;
        }

        public void AcceptCoins(List<Coin> coins)
        {
            var validCoins = coins.Where(x => IsValidCoin(x)).ToList();

            CoinReturn.AddRange(coins.Where(x => !IsValidCoin(x)).ToList());

            AmountInserted = CalculateAmount(validCoins);

            AdjustDisplayBasedOffAmountInserted();
        }

        public decimal CalculateAmount(List<Coin> validCoins)
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

        public Product SelectProduct(Product product)
        {
            if (Products.Exists(x => x.Name == product.Name))
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
            }
            else
            {
                _display = GlobalConstants.SoldOut;
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

        public void ReturnCoins()
        {
            CoinReturn.AddRange(MakeChange(AmountInserted));
            _display = GlobalConstants.InsertCoin;
        }

        public string CheckDisplay()
        {
            if (_totalAmount < Products.Max(x => x.Price))
                return GlobalConstants.ExactChangeOnly;

            if (_display == GlobalConstants.ThankYou)
            {
                AmountInserted = 0m;
                _display = GlobalConstants.InsertCoin;

                return GlobalConstants.ThankYou;
            }

            if (_display.Contains(GlobalConstants.Price))
            {
                var displayCopy = _display;

                AdjustDisplayBasedOffAmountInserted();

                return displayCopy;
            }

            if (_display.Contains(GlobalConstants.SoldOut))
            {
                AdjustDisplayBasedOffAmountInserted();

                return GlobalConstants.SoldOut;
            }

            return _display;
        }

        private void AdjustDisplayBasedOffAmountInserted()
        {
            if (AmountInserted == 0m)
                _display = GlobalConstants.InsertCoin;
            else
                _display = AmountInserted.ToString("C");
        }

        public bool IsValidCoin(Coin coin)
        {
            return (coin.IsQuarter() || coin.IsDime() || coin.IsNickel());
        }
    }
}
