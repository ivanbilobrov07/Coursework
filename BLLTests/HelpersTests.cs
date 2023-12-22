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
    public class HelpersTests
    {
        [TestMethod()]
        public void isValidIndexTest()
        {
            // Arrange
            string[] list = new string[7];
            int index1 = -1;
            int index2 = 0;
            int index3 = 6;
            int index4 = 7;

            // Act
            bool result1 = Helpers.isValidIndex(index1, list.Length);
            bool result2 = Helpers.isValidIndex(index2, list.Length);
            bool result3 = Helpers.isValidIndex(index3, list.Length);
            bool result4 = Helpers.isValidIndex(index4, list.Length);

            // Assert
            Assert.AreEqual(result1, false);
            Assert.AreEqual(result2, true);
            Assert.AreEqual(result3, true);
            Assert.AreEqual(result4, false);
        }       
    }
}