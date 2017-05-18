using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VendingMachineKata;
using System.Collections.Generic;

namespace VendingMachineKataTests
{
    [TestClass]
    public class AcceptCoinsTests
    {
        private VendingMachine vendingMachine { get; set; }
        private List<Coin> validCoins { get; set; }
        private List<Coin> invalidCoins { get; set; }
        private string validCoinsAmount { get; set; }

        [TestInitialize()]
        public void Initialize()
        {
            vendingMachine = new VendingMachine(0m, new List<Coin>(), GlobalConstants.InsertCoin, new List<Product>(), 1000m);

            validCoins = new List<Coin>
            {
                new Coin(GlobalConstants.QuarterGrams, GlobalConstants.QuarterDiameter),
                new Coin(GlobalConstants.DimeGrams, GlobalConstants.DimeDiameter),
                new Coin(GlobalConstants.NickelGrams, GlobalConstants.NickelDiameter)
            };

            validCoinsAmount = Helper.CalculateAmount(validCoins).ToString("C");

            invalidCoins = new List<Coin>
            {
                new Coin(1, 1),
                new Coin(2, 2),
                new Coin(3, 3)
            };
        }

        [TestMethod]
        public void DisplaysInsertCoinWhenNoCoinsInserted()
        {
            var display = vendingMachine.CheckDisplay();

            Assert.AreEqual(GlobalConstants.InsertCoin, display);
        }

        [TestMethod]
        public void DisplaysProperAmountWhenOnlyValidCoinsInserted()
        {
            vendingMachine.AcceptCoins(validCoins);

            var display = vendingMachine.CheckDisplay();

            Assert.AreEqual(validCoinsAmount, display);
        }

        [TestMethod]
        public void RejectsCoinsWhenOnlyInvalidCoinsInserted()
        {
            vendingMachine.AcceptCoins(invalidCoins);

            var display = vendingMachine.CheckDisplay();

            Assert.AreEqual(GlobalConstants.InsertCoin, display);
            CollectionAssert.AreEqual(invalidCoins, vendingMachine.CoinReturn);
        }

        [TestMethod]
        public void RejectsInvalidCoinsAndAcceptValidCoins()
        {
            vendingMachine.AcceptCoins(invalidCoins);
            vendingMachine.AcceptCoins(validCoins);

            var display = vendingMachine.CheckDisplay();

            Assert.AreEqual(validCoinsAmount, display);
            CollectionAssert.AreEqual(invalidCoins, vendingMachine.CoinReturn);
        }
    }
}
