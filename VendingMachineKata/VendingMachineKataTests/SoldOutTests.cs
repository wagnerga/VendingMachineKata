using System;
using NUnit.Framework;
using VendingMachineKata;
using System.Collections.Generic;

namespace VendingMachineKataTests
{
    [TestFixture]
    public class SoldOutTests
    {
        private VendingMachine vendingMachine { get; set; }
        private List<Coin> colaCoins { get; set; }
        private List<Coin> chipCoins { get; set; }
        private List<Coin> candyCoins { get; set; }

        [SetUp]
        public void Initialize()
        {
            vendingMachine = new VendingMachine(0m,
                new List<Coin>(),
                GlobalConstants.InsertCoin,
                new List<Product> { new Product("cola", 1m) },
                1000m);

            colaCoins = new List<Coin>
            {
                new Coin(GlobalConstants.QuarterGrams, GlobalConstants.QuarterDiameter),
                new Coin(GlobalConstants.QuarterGrams, GlobalConstants.QuarterDiameter),
                new Coin(GlobalConstants.QuarterGrams, GlobalConstants.QuarterDiameter),
                new Coin(GlobalConstants.QuarterGrams, GlobalConstants.QuarterDiameter)
            };

            chipCoins = new List<Coin>
            {
                new Coin(GlobalConstants.QuarterGrams, GlobalConstants.QuarterDiameter),
                new Coin(GlobalConstants.QuarterGrams, GlobalConstants.QuarterDiameter)
            };

            candyCoins = new List<Coin>
            {
                new Coin(GlobalConstants.QuarterGrams, GlobalConstants.QuarterDiameter),
                new Coin(GlobalConstants.QuarterGrams, GlobalConstants.QuarterDiameter),
                new Coin(GlobalConstants.DimeGrams, GlobalConstants.DimeDiameter),
                new Coin(GlobalConstants.NickelGrams, GlobalConstants.NickelDiameter)
            };
        }

        [Test]
        public void OutOfStockDisplaysSoldOutThenDisplaysInsertedAmountWhenAmountInserted()
        {
            var product = new Product("candy", 0.65m);

            vendingMachine.AcceptCoins(colaCoins);

            var display1 = vendingMachine.CheckDisplay();

            Assert.AreEqual(vendingMachine.CalculateAmount(colaCoins).ToString("C"), display1);

            var dispensedProduct = vendingMachine.SelectProduct(product);

            Assert.AreEqual(null, dispensedProduct);

            var display2 = vendingMachine.CheckDisplay();

            Assert.AreEqual(GlobalConstants.SoldOut, display2);

            var display3 = vendingMachine.CheckDisplay();

            Assert.AreEqual(vendingMachine.CalculateAmount(colaCoins).ToString("C"), display3);
        }

        [Test]
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
