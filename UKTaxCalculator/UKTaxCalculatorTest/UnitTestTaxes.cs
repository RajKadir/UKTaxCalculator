using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UKTaxCalculator;

namespace UKTaxCalculatorTest
{
    [TestClass]
    public class UnitTestTaxes
    {
        [TestMethod]
        public void TestTaxBands()
        {
            double grossIncome = 45000;
            Assert.AreEqual(Program.CalculateTaxBands(grossIncome), 0.2, "Tax bands not correct");
        }
    }
}
