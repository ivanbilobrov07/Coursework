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

        public void Setup()
        {
            realEstateService = new RealEstateService("json");
            realEstateService.Clear();
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
            Assert.Fail();
        }

        [TestMethod()]
        public void RemoveRealEstateTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void PrintTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void PrintTest1()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetRealEstateTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void AddRealEstateToCustomerTest()
        {
            Assert.Fail();
        }
    }
}