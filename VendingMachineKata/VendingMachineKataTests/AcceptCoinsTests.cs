using System;
using NUnit.Framework;
using VendingMachineKata;
using System.Collections.Generic;

namespace VendingMachineKataTests
{
    [TestFixture]
    public class AcceptCoinsTests
    {
        private VendingMachine vendingMachine { get; set; }
        private List<Coin> validCoins { get; set; }
        private List<Coin> invalidCoins { get; set; }

        [SetUp]
        public void Initialize()
        {
            vendingMachine = new VendingMachine(0m,
                new List<Coin>(),
                GlobalConstants.InsertCoin,
                new List<Product> { new Product("cola", 1m), new Product("chips", .5m), new Product("candy", .65m) },
                1000m);

            validCoins = new List<Coin>
            {
                new Coin(GlobalConstants.QuarterGrams, GlobalConstants.QuarterDiameter),
                new Coin(GlobalConstants.DimeGrams, GlobalConstants.DimeDiameter),
                new Coin(GlobalConstants.NickelGrams, GlobalConstants.NickelDiameter)
            };

            invalidCoins = new List<Coin>
            {
                new Coin(1, 1),
                new Coin(2, 2),
                new Coin(3, 3)
            };
        }

        [Test]
        public void DisplaysInsertCoinWhenNoCoinsInserted()
        {
            var display = vendingMachine.CheckDisplay();

            Assert.AreEqual(GlobalConstants.InsertCoin, display);
        }

        [Test]
        public void DisplaysProperAmountWhenOnlyValidCoinsInserted()
        {
            vendingMachine.AcceptCoins(validCoins);

            var display = vendingMachine.CheckDisplay();

            Assert.AreEqual(vendingMachine.CalculateAmount(validCoins).ToString("C"), display);
        }

        [Test]
        public void RejectsCoinsWhenOnlyInvalidCoinsInserted()
        {
            vendingMachine.AcceptCoins(invalidCoins);

            var display = vendingMachine.CheckDisplay();

            Assert.AreEqual(GlobalConstants.InsertCoin, display);
            CollectionAssert.AreEqual(invalidCoins, vendingMachine.CoinReturn);
        }

        [Test]
        public void RejectsInvalidCoinsAndAcceptValidCoins()
        {
            vendingMachine.AcceptCoins(invalidCoins);
            vendingMachine.AcceptCoins(validCoins);

            var display = vendingMachine.CheckDisplay();

            Assert.AreEqual(vendingMachine.CalculateAmount(validCoins).ToString("C"), display);
            CollectionAssert.AreEqual(invalidCoins, vendingMachine.CoinReturn);
        }
    }
}
