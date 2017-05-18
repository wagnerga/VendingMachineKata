using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VendingMachineKata;
using System.Collections.Generic;

namespace VendingMachineKataTests
{
    [TestClass]
    public class SoldOutTests
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
            vendingMachine = new VendingMachine(0m, new List<Coin>(), GlobalConstants.InsertCoin, new List<Product>
            {
                new Product("cola", 1m)
            });

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
        public void OutOfStockDisplaysSoldOutThenDisplaysInsertedAmountWhenAmountInserted()
        {
            var product = new Product("candy", 0.65m);

            vendingMachine.AcceptCoins(colaCoins);

            var display1 = vendingMachine.CheckDisplay();

            Assert.AreEqual(colaCoinsAmount, display1);

            var dispensedProduct = vendingMachine.SelectProduct(product);

            Assert.AreEqual(null, dispensedProduct);

            var display2 = vendingMachine.CheckDisplay();

            Assert.AreEqual(GlobalConstants.SoldOut, display2);

            var display3 = vendingMachine.CheckDisplay();

            Assert.AreEqual(colaCoinsAmount, display3);
        }

        [TestMethod]
        public void OutOfStockDisplaysSoldOutThenDisplaysInsertCoinWhenNoAmountInserted()
        {
            var product = new Product("chips", 0.5m);

            var dispensedProduct = vendingMachine.SelectProduct(product);

            Assert.AreEqual(null, dispensedProduct);

            var display1 = vendingMachine.CheckDisplay();

            Assert.AreEqual(GlobalConstants.SoldOut, display1);

            var display2 = vendingMachine.CheckDisplay();

            Assert.AreEqual(GlobalConstants.InsertCoin, display2);
        }
    }
}
