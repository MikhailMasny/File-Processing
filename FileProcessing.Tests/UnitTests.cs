using Microsoft.VisualStudio.TestTools.UnitTesting;
using FileProcessing.BL.Controllers.Implementations;
using System;
using System.Collections.Generic;
using FileProcessing.BL.Models;

namespace FileProcessing.BL.Tests
{
    [TestClass()]
    public class UnitTests
    {
        [TestMethod()]
        public void CheckForSpecifiedPathTest()
        {
            // Arrange
            var dataStructure = new DataStructure
            {
                Input_address = Guid.NewGuid().ToString()
            };
            var fileSystemController = new FileSystemController(dataStructure);

            // Act
            var result = fileSystemController.CheckForSpecifiedPath();

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod()]
        public void GetListOfFilesTest()
        {
            // Arrange
            var dataStructure = new DataStructure
            {
                Input_address = Guid.NewGuid().ToString()
            };
            var fileSystemController = new FileSystemController(dataStructure);

            // Act
            var result = fileSystemController.GetListOfFiles();

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod()]
        public void ProcessInputDataTest_Return_True()
        {
            // Arrange
            var listOfInputData = new List<string>();

            for (int i = 0; i < 10; i++)
            {
                listOfInputData.Add($"{Guid.NewGuid().ToString()},{i}");
            }

            var dataStructure = new DataStructure
            {
                ListOfInputData = listOfInputData
            };
            var fileSystemController = new FileSystemController(dataStructure);

            // Act
            var result = fileSystemController.ProcessInputData();

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void WriteDataToFileTest_Return_True()
        {
            // Arrange
            var listOfProcessedData = new Dictionary<string, int>();

            for (int i = 0; i < 10; i++)
            {
                listOfProcessedData.Add(Guid.NewGuid().ToString(), i);
            }

            var dataStructure = new DataStructure
            {
                ListOfProcessedData = listOfProcessedData
            };
            var fileSystemController = new FileSystemController(dataStructure);

            // Act
            var result = fileSystemController.WriteDataToFile();

            // Assert
            Assert.IsTrue(result);
        }
    }
}