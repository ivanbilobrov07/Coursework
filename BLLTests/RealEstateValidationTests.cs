using Microsoft.VisualStudio.TestTools.UnitTesting;
using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Tests
{
    [TestClass()]
    public class RealEstateValidationTests
    {
        [TestMethod()]
        public void isValidCityTest()
        {
            // Arrange
            string city = "Kyiv";
            string invaliCity = "A";

            // Act
            bool result = RealEstateValidation.isValidCity(city);

            // Assert
            Assert.AreEqual(result, true);
            Assert.ThrowsException<ValidationException>(() => RealEstateValidation.isValidCity(invaliCity));
        }

        [TestMethod()]
        public void isValidAddressTest()
        {
            // Arrange
            string address = "Some address";
            string invaliAddress = "qwe";

            // Act
            bool result = RealEstateValidation.isValidAddress(address);

            // Assert
            Assert.AreEqual(result, true);
            Assert.ThrowsException<ValidationException>(() => RealEstateValidation.isValidAddress(invaliAddress));
        }

        [TestMethod()]
        public void isValidPriceTest()
        {
            // Arrange
            int price = 1222;
            int invaliPrice = -39;

            // Act
            bool result = RealEstateValidation.isValidPrice(price);

            // Assert
            Assert.AreEqual(result, true);
            Assert.ThrowsException<ValidationException>(() => RealEstateValidation.isValidPrice(invaliPrice));
        }

        [TestMethod()]
        public void isValidTypesTest()
        {
            // Arrange
            string[] types1 = { "1-room", "flat" };
            string[] types2 = { "2-room", "house" };
            string[] invalidTypes1 = { "1-room", "2-room" };
            string[] invalidTypes2 = { "flat", "house" };

            // Act
            bool result1 = RealEstateValidation.isValidTypes(types1);
            bool result2 = RealEstateValidation.isValidTypes(types2);

            // Assert
            Assert.AreEqual(result1, true);
            Assert.AreEqual(result2, true);
            Assert.ThrowsException<ValidationException>(() => RealEstateValidation.isValidTypes(invalidTypes1));
            Assert.ThrowsException<ValidationException>(() => RealEstateValidation.isValidTypes(invalidTypes2));
        }
    }
}