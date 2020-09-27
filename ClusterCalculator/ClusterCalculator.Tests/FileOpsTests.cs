using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClusterCalculator.Tests
{
    public class FileOpsTests
    {

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void FileHeaderRetreivedSuccessfully()
        {
            //Arrange
            string resultLine;

            //Act
            resultLine = FileOps.GetHeader("testHeader1.csv");

            //Assert
            Assert.AreEqual("This is a test header", resultLine);
        }

        [Test]
        public void FileHeadersRetreivedSuccessfully()
        {
            //Arrange
            string [] headersList;
            string[] headersKey = { "this", "is", "a","test","header"};
            int counter = 0;

            //Act
            headersList = FileOps.GetHeaders("testHeader2.csv");

            //Assert
            foreach(string header in headersKey)
            {
                Assert.AreEqual(header, headersList[counter]);
                counter++;
            }
        }
    }
}
