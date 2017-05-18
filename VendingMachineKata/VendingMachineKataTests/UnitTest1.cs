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
        private string validCoinsValue { get; set; }

        [TestInitialize()]
        public void Initialize()
        {
            vendingMachine = new VendingMachine(new List<Coin>());

            validCoins = new List<Coin>
            {
                new Coin(GlobalConstants.QuarterGrams, GlobalConstants.QuarterDiameter),
                new Coin(GlobalConstants.DimeGrams, GlobalConstants.DimeDiameter),
                new Coin(GlobalConstants.NickelGrams, GlobalConstants.NickelDiameter)
            };

            validCoinsValue = (GlobalConstants.QuarterValue + GlobalConstants.DimeValue + GlobalConstants.NickelValue).ToString("C");

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
            var display = vendingMachine.GetDisplay();

            Assert.AreEqual(display, GlobalConstants.NoCoinsDisplay);
        }

        [TestMethod]
        public void DisplaysProperAmountWhenOnlyValidCoinsInserted()
        {
            vendingMachine.AcceptCoins(validCoins);

            var display = vendingMachine.GetDisplay();

            Assert.AreEqual(display, validCoinsValue);
        }

        [TestMethod]
        public void RejectsCoinsWhenOnlyInvalidCoinsInserted()
        {
            vendingMachine.AcceptCoins(invalidCoins);

            var display = vendingMachine.GetDisplay();

            Assert.AreEqual(display, GlobalConstants.NoCoinsDisplay);
            CollectionAssert.AreEqual(invalidCoins, vendingMachine.CoinReturn);
        }

        [TestMethod]
        public void RejectsInvalidCoinsAndAcceptValidCoins()
        {
            vendingMachine.AcceptCoins(invalidCoins);
            vendingMachine.AcceptCoins(validCoins);

            var display = vendingMachine.GetDisplay();

            Assert.AreEqual(display, validCoinsValue);
            CollectionAssert.AreEqual(invalidCoins, vendingMachine.CoinReturn);
        }
    }
}
