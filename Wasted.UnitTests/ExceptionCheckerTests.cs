using System;
using NUnit.Framework;
using Wasted.Utils.Exceptions;

namespace Wasted.UnitTests
{
    public class ExceptionCheckerTest
    {
        [Test]
        public void CheckValidParams_ShouldThrowException_WhenThereIsEmptyString()
        {
            // Arrange
            string[] testStringArray = {"", "hello", " world"};

            // Act

            // Assert
            Assert.Throws<ArgumentNullException>(() => ExceptionChecker.CheckValidParams(testStringArray));
        }

        [Test]
        public void CheckValidParams_ShouldNotThrowAnyException_WhenAllStringsAreNotNullAndNotEmpty()
        {
            // Arrange
            string[] testStringArray = {"hello", "world", "!"};

            // Act

            // Assert
            Assert.DoesNotThrow(() => ExceptionChecker.CheckValidParams(testStringArray));
        }

        [Test]
        public void CheckValidParams_ShouldThrowException_WhenThereIsNullString()
        {
            // Arrange
            string[] testStringArray = {"hello", null, " world"};

            // Act

            // Assert
            Assert.Throws<ArgumentNullException>(() => ExceptionChecker.CheckValidParams(testStringArray));
        }
    }
}