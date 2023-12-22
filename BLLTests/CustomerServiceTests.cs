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
    public class CustomerServiceTests
    {
        RealEstateService? realEstateService;
        CustomerService? customerService;

        public void Setup()
        {
            realEstateService = new RealEstateService("json", "testRE");
            customerService = new CustomerService("json", "testC");
            realEstateService.Clear();
            customerService.Clear();
        }

        [TestMethod()]
        public void AddCustomerTest()
        {
            // Arrange
            Setup();

            string firsName = "Ivan";
            string lastName = "Bilobrov";
            string email = "TestEmail@gmail.test";
            string bankAccount = "1010101032231234";

            // Act
            customerService.AddCustomer(firsName, lastName, email, bankAccount);

            // Assert
            Assert.AreEqual(customerService.DataList.Count(), 1);
            Assert.AreEqual(customerService.DataList[0].FirstName, firsName);
            Assert.AreEqual(customerService.DataList[0].LastName, lastName);
            Assert.AreEqual(customerService.DataList[0].Email, email);
            Assert.AreEqual(customerService.DataList[0].BankAccount, bankAccount);
        }

        [TestMethod()]
        public void RemoveCustomerTest()
        {
            // Arrange
            Setup();

            string firsName = "Ivan";
            string lastName = "Bilobrov";
            string email = "TestEmail@gmail.test";
            string bankAccount = "1010101032231234";

            // Act
            string addedCustomer = customerService.AddCustomer(firsName, lastName, email, bankAccount);
            Assert.AreEqual(customerService.DataList.Count(), 1);
            string deletedCustomer = customerService.RemoveCustomer(customerService.DataList[0].Id);

            // Assert
            Assert.AreEqual(customerService.DataList.Count(), 0);
            Assert.AreEqual(addedCustomer, deletedCustomer);
        }

        [TestMethod()]
        public void GetCustomerTest()
        {
            // Arrange
            Setup();

            string firsName = "Ivan";
            string lastName = "Bilobrov";
            string email = "TestEmail@gmail.test";
            string bankAccount = "1010101032231234";

            // Act
            customerService.AddCustomer(firsName, lastName, email, bankAccount);

            // Assert
            Assert.AreEqual(customerService.DataList[0], customerService.GetCustomer(customerService.DataList[0].Id));
        }

        [TestMethod()]
        public void AddRealEstateToCustomerTest()
        {
            // Arrange
            Setup();
            string city = "Kyiv";
            string address = "Test address";
            int price = 2220;
            string[] types = { "3-rooms", "flat" };

            string firsName = "Ivan";
            string lastName = "Bilobrov";
            string email = "TestEmail@gmail.test";
            string bankAccount = "1010101032231234";

            // Act
            realEstateService.AddRealEstate(city, address, price, types);
            customerService.AddCustomer(firsName, lastName, email, bankAccount);

            customerService.AddRealEstateToCustomer( customerService.DataList[0].Id, realEstateService.DataList[0].Id);

            // Assert
            Assert.IsTrue(customerService.DataList[0].RealEstates.Contains(realEstateService.DataList[0].Id));
        }
    }
}