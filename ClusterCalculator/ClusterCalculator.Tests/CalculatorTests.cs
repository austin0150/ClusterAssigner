using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClusterCalculator.Tests
{
    public class CalculatorTests
    {
        private Calculator calc;
        [SetUp]
        public void Setup()
        {
            calc = new Calculator();
        }

        [Test]
        public void CalcsDistanceCorrectly()
        {
            //Arrange
            double aaLat = 42.2808;
            double aaLong = 83.7430;
            double dLat = 42.3223;
            double dLong = 83.1763;
            double result;
            double realDist = 46886.358225708209;

            //Act
            result = calc.CalcDistance(aaLat, aaLong, dLat, dLong);

            //Assert
            Assert.AreEqual(realDist, result);
        }
    }
}
