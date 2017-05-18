using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachineKata
{
    public class Coin
    {
        private decimal _grams { get; set; }
        private decimal _diameter { get; set; }

        public Coin(decimal grams, decimal diameter)
        {
            _grams = grams;
            _diameter = diameter;
        }

        public bool IsQuarter()
        {
            return (_grams == GlobalConstants.QuarterGrams && _diameter == GlobalConstants.QuarterDiameter);
        }

        public bool IsDime()
        {
            return (_grams == GlobalConstants.DimeGrams && _diameter == GlobalConstants.DimeDiameter);
        }

        public bool IsNickel()
        {
            return (_grams == GlobalConstants.NickelGrams && _diameter == GlobalConstants.NickelDiameter);
        }
    }
}
