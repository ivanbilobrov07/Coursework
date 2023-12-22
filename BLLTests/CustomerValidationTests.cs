using Microsoft.VisualStudio.TestTools.UnitTesting;
using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BLL.Tests
{
    [TestClass()]
    public class CustomerValidationTests
    {
        [TestMethod()]
        public void isValidNameTest()
        {
            // Arrange
            string name = "Ivan";
            string invalidName = "A";

            // Act
            bool result = CustomerValidation.isValidName(name);

            // Assert
            Assert.AreEqual(result, true);
            Assert.ThrowsException<ValidationException>(() => CustomerValidation.isValidName(invalidName));

        }

        [TestMethod()]
        public void isValidEmailTest()
        {
            // Arrange
            string email = "test@gmail.com";
            string invalidEmail = "qweqweq@wdq";

            // Act
            bool result = CustomerValidation.isValidEmail(email);

            // Assert
            Assert.AreEqual(result, true);
            Assert.ThrowsException<ValidationException>(() => CustomerValidation.isValidEmail(invalidEmail));
        }

        [TestMethod()]
        public void isValidBankAccountTest()
        {
            // Arrange
            string bankAccount = "3212323123135424";
            string invalidBankAccount = "3212323q12335424";

            // Act
            bool result = CustomerValidation.isValidBankAccount(bankAccount);

            // Assert
            Assert.AreEqual(result, true);
            Assert.ThrowsException<ValidationException>(() => CustomerValidation.isValidBankAccount(invalidBankAccount));
        }
    }
}