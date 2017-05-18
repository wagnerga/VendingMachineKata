using System;
using NUnit.Framework;
using VendingMachineKata;
using System.Collections.Generic;

namespace VendingMachineKataTests
{
    [TestFixture]
    public class SelectProductTests
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
                new List<Product> { new Product("cola", 1m), new Product("chips", .5m), new Product("candy", .65m) },
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
        public void SelectColaWithExactPriceReturnsColaThenDisplaysThankYouThenDisplaysInsertCoinAndSetsCurrentAmountToZero()
        {
            var product = new Product("cola", 1.00m);

            vendingMachine.AcceptCoins(colaCoins);

            var display1 = vendingMachine.CheckDisplay();

            Assert.AreEqual(vendingMachine.CalculateAmount(colaCoins).ToString("C"), display1);

            var dispensedProduct = vendingMachine.SelectProduct(product);

            Assert.AreEqual(product, dispensedProduct);

            var display2 = vendingMachine.CheckDisplay();

            Assert.AreEqual(GlobalConstants.ThankYou, display2);

            var display3 = vendingMachine.CheckDisplay();

            Assert.AreEqual(GlobalConstants.InsertCoin, display3);
            Assert.AreEqual(0m, vendingMachine.AmountInserted);
        }

        [Test]
        public void SelectChipsWithExactPriceReturnsChipsThenDisplaysThankYouThenDisplaysInsertCoinAndSetsCurrentAmountToZero()
        {
            var product = new Product("chips", 0.50m);

            vendingMachine.AcceptCoins(chipCoins);

            var display1 = vendingMachine.CheckDisplay();

            Assert.AreEqual(vendingMachine.CalculateAmount(chipCoins).ToString("C"), display1);

            var dispensedProduct = vendingMachine.SelectProduct(product);

            Assert.AreEqual(product, dispensedProduct);

            var display2 = vendingMachine.CheckDisplay();

            Assert.AreEqual(GlobalConstants.ThankYou, display2);

            var display3 = vendingMachine.CheckDisplay();

            Assert.AreEqual(GlobalConstants.InsertCoin, display3);
            Assert.AreEqual(0m, vendingMachine.AmountInserted);
        }

        [Test]
        public void SelectCandyWithExactPriceReturnsCandyThenDisplaysThankYouThenDisplaysInsertCoinAndSetsCurrentAmountToZero()
        {
            var product = new Product("candy", 0.65m);

            vendingMachine.AcceptCoins(candyCoins);

            var display1 = vendingMachine.CheckDisplay();

            Assert.AreEqual(vendingMachine.CalculateAmount(candyCoins).ToString("C"), display1);

            var dispensedProduct = vendingMachine.SelectProduct(product);

            Assert.AreEqual(product, dispensedProduct);

            var display2 = vendingMachine.CheckDisplay();

            Assert.AreEqual(GlobalConstants.ThankYou, display2);

            var display3 = vendingMachine.CheckDisplay();

            Assert.AreEqual(GlobalConstants.InsertCoin, display3);
            Assert.AreEqual(0m, vendingMachine.AmountInserted);
        }

        [Test]
        public void SelectCandyWithNotEnoughMoneyDisplaysCandyPriceThenDisplaysCurrentAmount()
        {
            var product = new Product("candy", 0.65m);

            vendingMachine.AcceptCoins(chipCoins);

            var display1 = vendingMachine.CheckDisplay();

            Assert.AreEqual(vendingMachine.CalculateAmount(chipCoins).ToString("C"), display1);

            var dispensedProduct = vendingMachine.SelectProduct(product);

            Assert.AreEqual(null, dispensedProduct);

            var display2 = vendingMachine.CheckDisplay();

            Assert.AreEqual(string.Format("{0} {1}", GlobalConstants.Price, vendingMachine.CalculateAmount(candyCoins).ToString("C")), display2);

            var display3 = vendingMachine.CheckDisplay();

            Assert.AreEqual(vendingMachine.CalculateAmount(chipCoins).ToString("C"), display3);
        }
    }
}
