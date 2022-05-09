using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using WSUniversalLib;

namespace UnitTesting
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void Test_of_get_quantity_of_product()
        {
            Calculation calculation = new Calculation();

            int expected = 114147;
            int actual = calculation.GetQuantityForProduct(3, 1, 15, 20, 45);

            Assert.AreEqual(expected, actual);
        }
    }
}
