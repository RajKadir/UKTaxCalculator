using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UKTaxCalculator;

namespace UKTaxCalculatorTest
{
    [TestClass]
    public class UnitTestTaxes
    {
        [TestMethod]
        public void TestTwentyPercentTaxBand()
        {
            // 45k salary highest tax band should fall in the 20% (0.2)
            double grossIncome = 45000;
            Assert.AreEqual(Program.CalculateTaxBands(grossIncome), 0.2, "Tax bands not correct");
        }

        [TestMethod]
        public void TestFortyPercentTaxBand()
        {
            double grossIncome = 75000;
            Assert.AreEqual(Program.CalculateTaxBands(grossIncome), 0.4, "Tax bands not correct");
        }


    }
}
