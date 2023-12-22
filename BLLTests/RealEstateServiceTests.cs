using Microsoft.VisualStudio.TestTools.UnitTesting;
using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL.Tests
{
    [TestClass()]
    public class RealEstateServiceTests
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
        public void convertTypesTest()
        {
            // Arrange
            Setup();
            string[] types = { "1-room", "2-rooms", "3-rooms", "flat", "house", "private plot" };

            // Act
            List<RealEstateType> realEstateTypes = realEstateService.convertTypes(types);

            // Assert
            CollectionAssert.AreEqual(realEstateTypes, new List<RealEstateType> { RealEstateType.OneRoom, 
                RealEstateType.TwoRooms, RealEstateType.ThreeRooms, 
                RealEstateType.Flat, RealEstateType.House, 
                RealEstateType.PrivatePlot});
        }

        [TestMethod()]
        public void AddRealEstateTest()
        {
            // Arrange
            Setup();
            string city = "Kyiv";
            string address = "Test address";
            int price = 2220;
            string[] types = {"3-rooms", "flat" };

            // Act
            realEstateService.AddRealEstate(city, address, price, types);

            // Assert
            Assert.AreEqual(realEstateService.DataList.Count(), 1);
            Assert.AreEqual(realEstateService.DataList[0].City, city);
            Assert.AreEqual(realEstateService.DataList[0].Address, address);
            Assert.AreEqual(realEstateService.DataList[0].Price, price);
            CollectionAssert.AreEqual(realEstateService.DataList[0].Types, new List<RealEstateType> { RealEstateType.ThreeRooms, RealEstateType.Flat} );
        }

        [TestMethod()]
        public void RemoveRealEstateTest()
        {
            // Arrange
            Setup();
            string city = "Kyiv";
            string address = "Test address";
            int price = 2220;
            string[] types = { "3-rooms", "flat" };

            // Act
            string addedRealEstate = realEstateService.AddRealEstate(city, address, price, types);
            Assert.AreEqual(realEstateService.DataList.Count(), 1);
            string deletedRealEstate =  realEstateService.RemoveRealEstate(realEstateService.DataList[0].Id);

            // Assert
            Assert.AreEqual(realEstateService.DataList.Count(), 0);
            Assert.AreEqual(addedRealEstate, deletedRealEstate);
        }

        [TestMethod()]
        public void GetRealEstateTest()
        {
            // Arrange
            Setup();
            string city = "Kyiv";
            string address = "Test address";
            int price = 2220;
            string[] types = { "3-rooms", "flat" };

            // Act
            realEstateService.AddRealEstate(city, address, price, types);

            // Assert
            Assert.AreEqual(realEstateService.DataList[0], realEstateService.GetRealEstate(realEstateService.DataList[0].Id));
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

            realEstateService.AddRealEstateToCustomer(realEstateService.DataList[0].Id, customerService.DataList[0].Id);

            // Assert
            Assert.AreEqual(realEstateService.DataList[0].Owner, customerService.DataList[0].Id);
        }
    }
}