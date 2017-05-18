﻿using System;
using NUnit.Framework;
using VendingMachineKata;
using System.Collections.Generic;

namespace VendingMachineKataTests
{
    [TestFixture]
    public class MakeChangeTests
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
        public void WhenProductCostLessRemainingQuarterAndDimePutInCoinReturn()
        {
            var product = new Product("candy", 0.65m);

            vendingMachine.AcceptCoins(colaCoins);

            var display1 = vendingMachine.CheckDisplay();

            Assert.AreEqual(vendingMachine.CalculateAmount(colaCoins).ToString("C"), display1);

            var dispensedProduct = vendingMachine.SelectProduct(product);

            var coinReturn = new List<Coin> 
            { 
                new Coin(GlobalConstants.QuarterGrams, GlobalConstants.QuarterDiameter),
                new Coin(GlobalConstants.DimeGrams, GlobalConstants.DimeDiameter)
            };

            Assert.AreEqual(vendingMachine.CalculateAmount(coinReturn).ToString("C"), vendingMachine.CalculateAmount(vendingMachine.CoinReturn).ToString("C"));
        }

        [Test]
        public void WhenProductCostLessRemainingDimeAndNickelPutInCoinReturn()
        {
            var product = new Product("chips", 0.50m);

            vendingMachine.AcceptCoins(candyCoins);

            var display1 = vendingMachine.CheckDisplay();

            Assert.AreEqual(vendingMachine.CalculateAmount(candyCoins).ToString("C"), display1);

            var dispensedProduct = vendingMachine.SelectProduct(product);

            var coinReturn = new List<Coin> 
            { 
                new Coin(GlobalConstants.DimeGrams, GlobalConstants.DimeDiameter),
                new Coin(GlobalConstants.NickelGrams, GlobalConstants.NickelDiameter)
            };

            Assert.AreEqual(vendingMachine.CalculateAmount(coinReturn).ToString("C"), vendingMachine.CalculateAmount(vendingMachine.CoinReturn).ToString("C"));
        }

        [Test]
        public void WhenProductCostLessRemainingTwoQuartersPutInCoinReturn()
        {
            var product = new Product("chips", 0.50m);

            vendingMachine.AcceptCoins(colaCoins);

            var display1 = vendingMachine.CheckDisplay();

            Assert.AreEqual(vendingMachine.CalculateAmount(colaCoins).ToString("C"), display1);

            var dispensedProduct = vendingMachine.SelectProduct(product);

            var coinReturn = new List<Coin> 
            { 
                new Coin(GlobalConstants.QuarterGrams, GlobalConstants.QuarterDiameter),
                new Coin(GlobalConstants.QuarterGrams, GlobalConstants.QuarterDiameter)
            };

            Assert.AreEqual(vendingMachine.CalculateAmount(coinReturn).ToString("C"), vendingMachine.CalculateAmount(vendingMachine.CoinReturn).ToString("C"));
        }
    }
}
