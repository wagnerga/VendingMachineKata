using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VendingMachineKata;
using System.Collections.Generic;

namespace VendingMachineKataTests
{
    [TestClass]
    public class ExactChangeOnlyTests
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
                new Product("cola", 1m),
                new Product("chips", .5m),
                new Product("candy", .65m)
            }, .25m);

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
        public void DisplaysExactChangeOnlyWhenNotAbleToMakeChangeForAnyProducts()
        {
            var display = vendingMachine.CheckDisplay();

            Assert.AreEqual(GlobalConstants.ExactChangeOnly, display);
        }
    }
}
