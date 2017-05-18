using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VendingMachineKata;
using System.Collections.Generic;

namespace VendingMachineKataTests
{
    [TestClass]
    public class MakeChangeTests
    {
        private VendingMachine vendingMachine { get; set; }
        private List<Coin> colaCoins { get; set; }
        private string colaCoinsAmount { get; set; }
        private List<Coin> chipCoins { get; set; }
        private string chipCoinsAmount { get; set; }
        private List<Coin> candyCoins { get; set; }
        private string candyCoinsAmount { get; set; }

        [TestInitialize()]
        public void Initialize()
        {
            vendingMachine = new VendingMachine(0m, new List<Coin>(), GlobalConstants.InsertCoin, new List<Product>{
                new Product("cola", 1m),
                new Product("chips", .5m),
                new Product("candy", .65m)
            }, 1000m);

            colaCoins = new List<Coin>
            {
                new Coin(GlobalConstants.QuarterGrams, GlobalConstants.QuarterDiameter),
                new Coin(GlobalConstants.QuarterGrams, GlobalConstants.QuarterDiameter),
                new Coin(GlobalConstants.QuarterGrams, GlobalConstants.QuarterDiameter),
                new Coin(GlobalConstants.QuarterGrams, GlobalConstants.QuarterDiameter)
            };

            colaCoinsAmount = Helper.CalculateAmount(colaCoins).ToString("C");

            chipCoins = new List<Coin>
            {
                new Coin(GlobalConstants.QuarterGrams, GlobalConstants.QuarterDiameter),
                new Coin(GlobalConstants.QuarterGrams, GlobalConstants.QuarterDiameter)
            };

            chipCoinsAmount = Helper.CalculateAmount(chipCoins).ToString("C");

            candyCoins = new List<Coin>
            {
                new Coin(GlobalConstants.QuarterGrams, GlobalConstants.QuarterDiameter),
                new Coin(GlobalConstants.QuarterGrams, GlobalConstants.QuarterDiameter),
                new Coin(GlobalConstants.DimeGrams, GlobalConstants.DimeDiameter),
                new Coin(GlobalConstants.NickelGrams, GlobalConstants.NickelDiameter)
            };

            candyCoinsAmount = Helper.CalculateAmount(candyCoins).ToString("C");
        }

        [TestMethod]
        public void WhenProductCostLessRemainingQuarterAndDimePutInCoinReturn()
        {
            var product = new Product("candy", 0.65m);

            vendingMachine.AcceptCoins(colaCoins);

            var display1 = vendingMachine.CheckDisplay();

            Assert.AreEqual(colaCoinsAmount, display1);

            var dispensedProduct = vendingMachine.SelectProduct(product);

            var coinReturn = new List<Coin> 
            { 
                new Coin(GlobalConstants.QuarterGrams, GlobalConstants.QuarterDiameter),
                new Coin(GlobalConstants.DimeGrams, GlobalConstants.DimeDiameter)
            };

            Assert.AreEqual(Helper.CalculateAmount(coinReturn), Helper.CalculateAmount(vendingMachine.CoinReturn));
        }

        [TestMethod]
        public void WhenProductCostLessRemainingDimeAndNickelPutInCoinReturn()
        {
            var product = new Product("chips", 0.50m);

            vendingMachine.AcceptCoins(candyCoins);

            var display1 = vendingMachine.CheckDisplay();

            Assert.AreEqual(candyCoinsAmount, display1);

            var dispensedProduct = vendingMachine.SelectProduct(product);

            var coinReturn = new List<Coin> 
            { 
                new Coin(GlobalConstants.DimeGrams, GlobalConstants.DimeDiameter),
                new Coin(GlobalConstants.NickelGrams, GlobalConstants.NickelDiameter)
            };

            Assert.AreEqual(Helper.CalculateAmount(coinReturn), Helper.CalculateAmount(vendingMachine.CoinReturn));
        }

        [TestMethod]
        public void WhenProductCostLessRemainingTwoQuartersPutInCoinReturn()
        {
            var product = new Product("chips", 0.50m);

            vendingMachine.AcceptCoins(colaCoins);

            var display1 = vendingMachine.CheckDisplay();

            Assert.AreEqual(colaCoinsAmount, display1);

            var dispensedProduct = vendingMachine.SelectProduct(product);

            var coinReturn = new List<Coin> 
            { 
                new Coin(GlobalConstants.QuarterGrams, GlobalConstants.QuarterDiameter),
                new Coin(GlobalConstants.QuarterGrams, GlobalConstants.QuarterDiameter)
            };

            Assert.AreEqual(Helper.CalculateAmount(coinReturn), Helper.CalculateAmount(vendingMachine.CoinReturn));
        }
    }
}
